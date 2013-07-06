using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

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
