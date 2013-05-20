using System.Runtime.Serialization;

namespace UltimateTeam.Toolkit.Model
{
	[DataContract]
	public class ApiError
	{
		[DataMember(Name = "reason")]
		public string Reason { get; set; }

		[DataMember(Name = "message")]
		public string Message { get; set; }

		[DataMember(Name = "code")]
		public uint Code { get; set; }
	}
}

