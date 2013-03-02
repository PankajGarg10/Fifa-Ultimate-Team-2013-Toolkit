using System.Threading.Tasks;
using UltimateTeam.Toolkit.Constant;
using UltimateTeam.Toolkit.Model;

namespace UltimateTeam.Toolkit.Request
{
    public class PurchasedItemsRequest : RequestBase
    {
        public async Task<PurchasedItemsResponse> GetPurchasedItems()
        {
            var response = await Client.SendAsync(CreateRequestMessage(" ", Resources.PurchasedItems, HttpMethod.Get));
            response.EnsureSuccessStatusCode();

            return await Deserialize<PurchasedItemsResponse>(response);
        }
    }
}