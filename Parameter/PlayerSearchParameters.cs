using System;
using UltimateTeam.Toolkit.Model;

namespace UltimateTeam.Toolkit.Parameter
{
    public class PlayerSearchParameters : SearchParameters
    {
        public PlayerSearchParameters()
            : base(ResourceType.Player)
        {
        }

        public uint League { get; set; }

        public Level Level { get; set; }

        public string Position { get; set; }

        public uint Nation { get; set; }

        public string Formation { get; set; }

        public uint Team { get; set; }

        public uint MinBuy { get; set; }

        public uint MaxBuy { get; set; }

        public uint MinBid { get; set; }

        public uint MaxBid { get; set; }

        internal override string BuildUriString(ref string uriString)
        {
            if (League > 0)
                uriString += "&leag=" + League;

            SetLevel(ref uriString);

            if (Nation > 0)
                uriString += "&nat=" + Nation;

            if (!string.IsNullOrEmpty(Formation))
                uriString += "&form=" + Formation;

            if (Team > 0)
                uriString += "&team=" + Team;

            if (MinBuy > 0)
                uriString += "&minb=" + MinBuy;

            if (MaxBuy > 0)
                uriString += "&maxb=" + MaxBuy;

            if (MinBid > 0)
                uriString += "&micr=" + MinBid;

            if (MaxBid > 0)
                uriString += "&macr=" + MaxBid;

            SetPosition(ref uriString);

            uriString += "&type=" + Type.ToString().ToLower();

            return uriString;
        }

        private void SetLevel(ref string uriString)
        {
            switch (Level)
            {
                case Level.All:
                    break;
                case Level.Bronze:
                case Level.Silver:
                case Level.Gold:
                    uriString += "&lev=" + Level.ToString().ToLower();
                    break;
                default:
                    throw new ArgumentException("Level");
            }
        }

        private void SetPosition(ref string uriString)
        {
            if (!string.IsNullOrEmpty(Position))
                uriString += (
                                 Position == Parameter.Position.Defenders ||
                                 Position == Parameter.Position.Midfielders ||
                                 Position == Parameter.Position.Attackers
                                     ? "&zone="
                                     : "&pos=")
                             + Position;
        }
    }
}