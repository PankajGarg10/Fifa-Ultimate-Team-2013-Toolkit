using System;
using System.Net.Http;
using System.Threading.Tasks;
using UltimateTeam.Toolkit.Model;
using UltimateTeam.Toolkit.Parameter;

namespace UltimateTeam.Toolkit.Request
{
    public class SearchRequest : RequestBase
    {
        private const uint PageSize = 12;

        public async Task<AuctionResponse> SearchAsync(SearchParameters parameters)
        {
            if (parameters == null) throw new ArgumentNullException("parameters");
            if (parameters.Page < 1) throw new ArgumentException("Page must be > 0");

            var response = await Client.SendAsync(CreateRequestMessage(" ", BuildUriString(parameters), "GET"));
            response.EnsureSuccessStatusCode();

            return await Deserialize<AuctionResponse>(response);
        }

        private static string BuildUriString(SearchParameters parameters)
        {
            var uriString = string.Format(Resources.Search, (parameters.Page - 1) * PageSize, PageSize + 1);
            parameters.BuildUriString(ref uriString);

            return uriString;
        }
    }
}