using System;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Net.Http;
using System.Xml.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using UltimateTeam.Toolkit.Constant;
using UltimateTeam.Toolkit.Extension;
using UltimateTeam.Toolkit.Model;
using UltimateTeam.Toolkit.Service;
using HttpMethod = System.Net.Http.HttpMethod;

namespace UltimateTeam.Toolkit.Request
{
    public class LoginRequest : RequestBase
    {
        private IHasher _hasher;

        public IHasher Hasher
        {
            get { return _hasher ?? new Hasher(); }
            set
            {
                if (value == null) throw new ArgumentNullException("value");
                _hasher = value;
            }
        }

        public async Task LoginAsync(string username, string password, string securityAnswer)
        {
            if (string.IsNullOrEmpty(username)) throw new ArgumentException("username");
            if (string.IsNullOrEmpty(password)) throw new ArgumentException("password");
            if (string.IsNullOrEmpty(securityAnswer)) throw new ArgumentException("securityAnswer");

            var loginResponse = await LoginRequestAsync(username, password);
            var persona = await AccountInfoRequestAsync();
            var authResponse = await AuthenticationRequestAsync(loginResponse, persona);
            await ValidateRequestAsync(authResponse, securityAnswer);
        }

        private async Task<ValidateResponse> ValidateRequestAsync(AuthenticationResponse authResponse, string securityAnswer)
        {
            var questionUrl = new Uri(string.Format(Resources.Validate, Resources.Platform));
            var requestMessage = new HttpRequestMessage(HttpMethod.Post, questionUrl);
            requestMessage.Headers.TryAddWithoutValidation(NonStandardHttpHeaders.SessionId, authResponse.SessionId);
            requestMessage.Headers.TryAddWithoutValidation(NonStandardHttpHeaders.EmbedError, "true");
            requestMessage.Content = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                { "answer", Hasher.Hash(securityAnswer) }
            });

            var response = await Client.SendAsync(requestMessage);
            response.EnsureSuccessStatusCode();

            return await Deserialize<ValidateResponse>(response);
        }

        private async Task<AuthenticationResponse> AuthenticationRequestAsync(LoginResponse loginResponse, Persona persona)
        {
            var authJson = string.Format(@"{{ ""isReadOnly"": false, ""sku"": ""393A0001"", ""clientVersion"": 3, ""nuc"": {0}, ""nucleusPersonaId"": {1}, ""nucleusPersonaDisplayName"": ""{2}"", ""nucleusPersonaPlatform"": ""{3}"", ""locale"": ""en-GB"", ""method"": ""idm"", ""priorityLevel"":4, ""identification"": {{ ""EASW-Token"": """" }} }}",
                    loginResponse.Player.NucleusId,
                    persona.PersonaId,
                    persona.PersonaName,
                    persona.UserClubList.OrderByDescending(club => club.LastAccessTime).First().Platform
                    );
            var authUrl = new Uri(string.Format(Resources.Auth, Resources.Platform));
            var response = await Client.PostAsync(authUrl, new StringContent(authJson, Encoding.UTF8, "application/json"));
            response.EnsureSuccessStatusCode();

            var authenticationResponse = await Deserialize<AuthenticationResponse>(response);
            SessionId = authenticationResponse.SessionId;
            Resources.FutHostName = string.Format("{0}://{1}", authenticationResponse.Protocol, authenticationResponse.IpPort);

            return authenticationResponse;
        }

        private async Task<Persona> AccountInfoRequestAsync()
        {
            try
            {
                return await DownloadPersonaAsync(Resources.AccountInfoXbox);
            }
            catch (NullReferenceException)
            {
                // Catch it in case we can't find any user accounts, in that case we'll try the next URL.
                Resources.Platform = "ps3";
            }

            return await DownloadPersonaAsync(Resources.AccountInfoPc);
        }

        private async Task<Persona> DownloadPersonaAsync(string url)
        {
            var accountUrl = new Uri(string.Format(url, DateTime.UtcNow.ToUnixTimestamp()));
            var response = await Client.GetAsync(accountUrl);
            response.EnsureSuccessStatusCode();

            var accounts = await Deserialize<UserAccounts>(response);

            return accounts.UserAccountInfo.Personas.First();
        }

        private async Task<LoginResponse> LoginRequestAsync(string username, string password)
        {
            var loginUrl = new Uri(Resources.Login);
            var content = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                { "email", username },
                { "password", password },
                { "stay-signed", "ON"}
            });

            var response = await Client.PostAsync(loginUrl, content);
            response.EnsureSuccessStatusCode();

            var xml = XDocument.Parse(await response.Content.ReadAsStringAsync());

            var login = (from x in xml.Descendants("login")
                         let player = x.Element("player")
                         let preferredPersona = player.Element("preferredPersona")
                         select new LoginResponse
                         {
                             Success = (int)x.Element("success"),
                             Player = new Player
                             {
                                 Id = (int)player.Element("id"),
                                 NucleusId = (long)player.Element("nucleusId"),
                                 Email = (string)player.Element("email"),
                                 PreferredPersona = new PreferredPersona
                                 {
                                     Id = (long)preferredPersona.Element("id"),
                                     Gamertag = (string)preferredPersona.Element("gamertag"),
                                     Platform = (string)preferredPersona.Element("platform")
                                 }
                             }
                         })
                         .First();

            return login;
        }
    }
}