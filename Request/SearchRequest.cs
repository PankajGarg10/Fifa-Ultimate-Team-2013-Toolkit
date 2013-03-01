﻿using System;
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

            var searchUri = BuildUri(parameters);
            var requestMessage = new HttpRequestMessage(HttpMethod.Post, searchUri)
            {
                Content = new StringContent(" ")
            };
            requestMessage.Headers.TryAddWithoutValidation("X-Ut-Sid", SessionId);
            requestMessage.Headers.TryAddWithoutValidation("x-http-method-override", "GET");

            var response = await Client.SendAsync(requestMessage);
            response.EnsureSuccessStatusCode();

            var auctionResponse = JsonDeserializer.Deserialize<AuctionResponse>(await response.Content.ReadAsStreamAsync());

            return auctionResponse;
        }

        private static Uri BuildUri(SearchParameters parameters)
        {
            var uriString = string.Format(Resources.Search, (parameters.Page - 1) * PageSize, PageSize + 1);
            parameters.BuildUriString(ref uriString);

            return new Uri(uriString);
        }
    }
}