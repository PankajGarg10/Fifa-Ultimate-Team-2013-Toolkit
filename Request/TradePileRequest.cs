using System.Threading.Tasks;
using UltimateTeam.Toolkit.Model;

namespace UltimateTeam.Toolkit.Request
{
    public class TradePileRequest : RequestBase
    {
        public async Task<TradePileResponse> SendToTradePile(ItemData itemData)
        {
            var response = await Client.SendAsync(CreateRequestMessage(
                string.Format("{{\"itemData\":[{{\"id\":\"{0}\",\"pile\":\"trade\"}}]}}", itemData.Id),
                Resources.TradePile,
                "PUT"));
            response.EnsureSuccessStatusCode();

            return await Deserialize<TradePileResponse>(response);
        }
    }
}