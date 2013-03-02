using System.Collections.Generic;
using System.Runtime.Serialization;

namespace UltimateTeam.Toolkit.Model
{
    [DataContract]
    public class TradePileResponse
    {
        [DataMember(Name = "itemData")]
        public List<TradePileItemData> ItemData { get; set; }
    }
}