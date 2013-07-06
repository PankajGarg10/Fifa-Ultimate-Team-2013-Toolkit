using System.Threading.Tasks;
using UltimateTeam.Toolkit.Constant;
using UltimateTeam.Toolkit.Model;

namespace UltimateTeam.Toolkit.Request
{
    public class QuickSellRequest : RequestBase
    {
        public async Task<QuickSellResponse> QuickSell(long id)
        {
            var response = await Client.SendAsync(CreateRequestMessage(" ", BuildUriString(id), HttpMethod.Delete));
            response.EnsureSuccessStatusCode();

            return await Deserialize<QuickSellResponse>(response);
        }

        private string BuildUriString(long id)
        {
            return Resources.FutHostName + Resources.QuickSell + id;
        }
    }
}
