using System.Collections.Generic;
using System.Runtime.Serialization;

namespace UltimateTeam.Toolkit.Model
{
    [DataContract]
    public class PurchasedItemsResponse
    {
        [DataMember(Name = "itemData")]
        public List<ItemData> ItemData { get; set; }
    }
}