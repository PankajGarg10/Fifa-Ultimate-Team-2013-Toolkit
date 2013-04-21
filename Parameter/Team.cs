using System.Collections.Generic;

namespace UltimateTeam.Toolkit.Parameter
{
    public class Team : SearchParameterBase<uint>
    {
        public const uint ManchesterUnited = 11;

		public const uint BorussiaDortmund = 22;

        private Team(string description, uint value)
        {
            Description = description;
            Value = value;
        }

        public static IEnumerable<Team> GetAll()
        {
            yield return new Team("Manchester United", ManchesterUnited);
			yield return new Team("Borussia Dortmund", BorussiaDortmund);
        }
    }
}