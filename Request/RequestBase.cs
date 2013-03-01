using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using UltimateTeam.Toolkit.Service;

namespace UltimateTeam.Toolkit.Request
{
    public abstract class RequestBase
    {
        private IJsonDeserializer _jsonDeserializer;
        private static readonly CookieContainer CookieContainer = new CookieContainer();
        protected static string SessionId;
        protected readonly HttpClient Client;

        public IJsonDeserializer JsonDeserializer
        {
            private get { return _jsonDeserializer ?? new JsonDeserializer(); }
            set
            {
                if (value == null) throw new ArgumentNullException("value");
                _jsonDeserializer = value;
            }
        }

        protected RequestBase()
        {
            var handler = new HttpClientHandler { CookieContainer = CookieContainer };
            Client = new HttpClient(handler);
            Client.DefaultRequestHeaders.ExpectContinue = false;
            Client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 6.2; WOW64) AppleWebKit/537.17 (KHTML, like Gecko) Chrome/24.0.1312.57 Safari/537.17");
            Client.DefaultRequestHeaders.Referrer = new Uri(Resources.Home);
        }

        protected internal HttpRequestMessage CreateRequestMessage(string content, string uriString, string httpMethodOverride)
        {
            var stringContent = new StringContent(content, Encoding.UTF8, "application/json");
            var requestMessage = new HttpRequestMessage(HttpMethod.Post, new Uri(uriString)) { Content = stringContent };
            requestMessage.Headers.TryAddWithoutValidation("X-Ut-Sid", SessionId);
            requestMessage.Headers.TryAddWithoutValidation("x-http-method-override", httpMethodOverride);

            return requestMessage;
        }

        protected async internal Task<T> Deserialize<T>(HttpResponseMessage responseMessage)
        {
            return JsonDeserializer.Deserialize<T>(await responseMessage.Content.ReadAsStreamAsync());
        }
    }
}