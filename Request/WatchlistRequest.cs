using System.Threading.Tasks;
using UltimateTeam.Toolkit.Model;
using UltimateTeam.Toolkit.Constant;

namespace UltimateTeam.Toolkit.Request
{
    public class WatchlistRequest : RequestBase
    {
        public async Task<WatchlistResponse> RequestWatchlist()
        {
            var response = await Client.SendAsync(CreateRequestMessage(" ", BuildUriString(), HttpMethod.Get));
            response.EnsureSuccessStatusCode();

            return await Deserialize<WatchlistResponse>(response);
        }

        private string BuildUriString()
        {
            return Resources.FutHostName + Resources.Watchlist;
        }
    }
}
