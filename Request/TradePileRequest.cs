using System.Threading.Tasks;
using UltimateTeam.Toolkit.Constant;
using UltimateTeam.Toolkit.Model;

namespace UltimateTeam.Toolkit.Request
{
    public class TradePileRequest : RequestBase
    {
        public async Task<TradePileResponse> SendToTradePileAsync(ItemData itemData)
        {
            var response = await Client.SendAsync(CreateRequestMessage(
                string.Format("{{\"itemData\":[{{\"id\":\"{0}\",\"pile\":\"trade\"}}]}}", itemData.Id),
                Resources.FutHostName + Resources.TradePile,
                HttpMethod.Put));
            response.EnsureSuccessStatusCode();

            return await Deserialize<TradePileResponse>(response);
        }

        public async Task<AuctionResponse> RequestTradepileAsync()
        {
            var response = await Client.SendAsync(CreateRequestMessage(" ", Resources.FutHostName + Resources.TradepileList, HttpMethod.Get));
            response.EnsureSuccessStatusCode();

            return await Deserialize<AuctionResponse>(response);
        }
    }
}