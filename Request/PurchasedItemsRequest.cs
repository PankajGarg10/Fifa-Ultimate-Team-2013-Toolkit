using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using UltimateTeam.Toolkit.Model;

namespace UltimateTeam.Toolkit.Request
{
    public class PurchasedItemsRequest : RequestBase
    {
        public async Task<PurchasedItemsResponse> GetPurchasedItems()
        {
            var uri = new Uri(Resources.PurchasedItems);
            var content = new StringContent(
                " ",
                Encoding.UTF8,
                "application/json");
            var requestMessage = new HttpRequestMessage(HttpMethod.Post, uri) { Content = content };
            requestMessage.Headers.TryAddWithoutValidation("X-Ut-Sid", SessionId);
            requestMessage.Headers.TryAddWithoutValidation("x-http-method-override", "GET");

            var response = await Client.SendAsync(requestMessage);
            response.EnsureSuccessStatusCode();

            var purchasedItemsResponse = JsonDeserializer.Deserialize<PurchasedItemsResponse>(await response.Content.ReadAsStreamAsync());

            return purchasedItemsResponse;
        }
    }
}