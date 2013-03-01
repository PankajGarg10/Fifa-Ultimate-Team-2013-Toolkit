using System.Threading.Tasks;
using UltimateTeam.Toolkit.Model;

namespace UltimateTeam.Toolkit.Request
{
    public class CreditsRequest : RequestBase
    {
        public async Task<CreditsResponse> GetCredits()
        {
            var response = await Client.SendAsync(CreateRequestMessage(" ", Resources.Credits, "GET"));
            response.EnsureSuccessStatusCode();

            return await Deserialize<CreditsResponse>(response);
        }
    }
}