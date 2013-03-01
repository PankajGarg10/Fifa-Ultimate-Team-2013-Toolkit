using System.Threading.Tasks;
using System.Collections.Generic;
using UltimateTeam.Toolkit.Model;

namespace UltimateTeam.Toolkit.Request
{
    public class TradeRequest : RequestBase
    {
        public async Task<AuctionResponse> GetTradeStatuses(IEnumerable<long> tradeIds)
        {
            var response = await Client.SendAsync(
                CreateRequestMessage(" ", string.Format(Resources.TradeStatus, string.Join("%2C", tradeIds)), "GET"));
            response.EnsureSuccessStatusCode();

            return await Deserialize<AuctionResponse>(response);
        }
    }
}