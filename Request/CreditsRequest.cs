using System.Threading.Tasks;
using UltimateTeam.Toolkit.Constant;
using UltimateTeam.Toolkit.Model;

namespace UltimateTeam.Toolkit.Request
{
    public class CreditsRequest : RequestBase
    {
        public async Task<CreditsResponse> GetCreditsAsync()
        {
            var response = await Client.SendAsync(CreateRequestMessage(" ", Resources.FutHostName + Resources.Credits, HttpMethod.Get));

			await EnsureSuccessfulResponse(response);

            return await Deserialize<CreditsResponse>(response);
        }
    }
}