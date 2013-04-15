using System.Runtime.Serialization;

namespace UltimateTeam.Toolkit.Model
{
    [DataContract]
    public class ListAuctionResponse
    {
        [DataMember(Name = "id")]
        public long TradeId { get; set; }
    }
}