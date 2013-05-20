using System.Threading.Tasks;
using UltimateTeam.Toolkit.Constant;
using UltimateTeam.Toolkit.Model;
using System.Net.Http;

namespace UltimateTeam.Toolkit.Request
{
    public class CreditsRequest : RequestBase
    {
        public async Task<CreditsResponse> GetCreditsAsync()
        {
            var response = await Client.SendAsync(CreateRequestMessage(" ", Resources.FutHostName + Resources.Credits, UltimateTeam.Toolkit.Constant.HttpMethod.Get));

			await EnsureSuccessfulResponse(response);

            return await Deserialize<CreditsResponse>(response);
        }
    }
}