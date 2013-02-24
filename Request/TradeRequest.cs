using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using UltimateTeam.Toolkit.Model;

namespace UltimateTeam.Toolkit.Request
{
    public class TradeRequest : RequestBase
    {
        public async Task<AuctionResponse> GetTradeStatuses(IEnumerable<long> tradeIds)
        {
            var uriString = string.Format(Resources.TradeStatus, string.Join("%2C", tradeIds));
            var uri = new Uri(uriString);
            var requestMessage = new HttpRequestMessage(HttpMethod.Post, uri) { Content = new StringContent(" ") };
            requestMessage.Headers.TryAddWithoutValidation("X-Ut-Sid", SessonId);
            requestMessage.Headers.TryAddWithoutValidation("x-http-method-override", "GET");
            
            var response = await Client.SendAsync(requestMessage);
            response.EnsureSuccessStatusCode();

            var auctionResponse = JsonDeserializer.Deserialize<AuctionResponse>(await response.Content.ReadAsStreamAsync());

            return auctionResponse;
        }
    }
}