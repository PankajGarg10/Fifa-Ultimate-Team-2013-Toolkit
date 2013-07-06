using System.Runtime.Serialization;

namespace UltimateTeam.Toolkit.Model
{
    [DataContract]
    public class QuickSellResponse
    {
        [DataMember(Name = "totalCredits")]
        public uint Credits { get; set; }
    }
}