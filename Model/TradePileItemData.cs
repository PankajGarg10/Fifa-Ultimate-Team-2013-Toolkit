using System.Runtime.Serialization;

namespace UltimateTeam.Toolkit.Model
{
    [DataContract]
    public class TradePileItemData
    {
        [DataMember(Name = "id")]
        public long Id { get; set; }

        [DataMember(Name = "pile")]
        public string Pile { get; set; }

        [DataMember(Name = "success")]
        public string Success { get; set; }
    }
}