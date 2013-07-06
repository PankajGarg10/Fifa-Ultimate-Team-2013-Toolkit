using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace UltimateTeam.Toolkit.Model
{
    [DataContract]
    public class QuickSellResponse
    {
        [DataMember(Name = "totalCredits")]
        public uint Credits { get; set; }
    }
}
