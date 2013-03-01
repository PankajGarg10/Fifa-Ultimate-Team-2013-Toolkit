using System.Threading.Tasks;
using UltimateTeam.Toolkit.Model;

namespace UltimateTeam.Toolkit.Request
{
    public class PurchasedItemsRequest : RequestBase
    {
        public async Task<PurchasedItemsResponse> GetPurchasedItems()
        {
            var response = await Client.SendAsync(CreateRequestMessage(" ", Resources.PurchasedItems, "GET"));
            response.EnsureSuccessStatusCode();

            return await Deserialize<PurchasedItemsResponse>(response);
        }
    }
}