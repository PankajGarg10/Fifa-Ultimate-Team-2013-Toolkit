using System.Collections.Generic;
using System.Runtime.Serialization;

namespace UltimateTeam.Toolkit.Model
{
    [DataContract]
    public class WatchlistResponse
    {
        [DataMember(Name = "auctionInfo")]
        public List<AuctionInfo> AuctionInfo { get; set; }

        [DataMember(Name = "credits")]
        public uint Credits { get; set; }

        [DataMember(Name = "total")]
        public uint Total { get; set; }
    }
}
