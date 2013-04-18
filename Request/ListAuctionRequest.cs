using System;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using UltimateTeam.Toolkit.Constant;
using UltimateTeam.Toolkit.Model;

namespace UltimateTeam.Toolkit.Request
{
    public class ListAuctionRequest : RequestBase
    {
        public ListAuctionRequest()
        {
            Client.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip"));
            Client.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("deflate"));
            Client.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("sdch"));
        }

        public async Task<ListAuctionResponse> ListAuctionAsync(long id, uint buyNowPrice = 0, AuctionDuration duration = AuctionDuration.OneHour, uint startingBid = 150)
        {
            if (id < 1) throw new ArgumentException("Invalid id", "id");
            if (buyNowPrice != 0 && buyNowPrice < startingBid) throw new ArgumentException("Buy now price can't be lower than starting bid", "buyNowPrice");
            if (startingBid < 150) throw new ArgumentOutOfRangeException("startingBid", "Starting bid can't be less than 150");
            // TODO: Validate starting bid

            var content = string.Format("{{\"itemData\":{{\"id\":{0}}},\"buyNowPrice\":{1},\"duration\":{2},\"startingBid\":{3}}}",
                id, buyNowPrice, (uint)duration, startingBid);
            var response = await Client.SendAsync(CreateRequestMessage(content, Resources.FutHostName + Resources.AuctionHouse, HttpMethod.Post));
            response.EnsureSuccessStatusCode();

            return await Deserialize<ListAuctionResponse>(response);
        }
    }
}