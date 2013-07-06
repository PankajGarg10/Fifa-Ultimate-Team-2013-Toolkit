namespace UltimateTeam.Toolkit.Constant
{
    internal class Resources
    {
        public static string Platform = "360";

        public const string Validate = "http://www.ea.com/p/fut/a/card-{0}/l/en_GB/s/p/ut/game/fifa13/phishing/validate";

        public const string Auth = "http://www.ea.com/p/fut/a/card-{0}/l/en_GB/s/p/ut/auth";

        public const string AccountInfo = "http://www.ea.com/p/fut/a/card-{0}/l/en_GB/s/p/ut/game/fifa13/user/accountinfo?timestamp={1}";

        public const string Login = "https://www.ea.com/intl/football/services/authenticate/login";

        public const string Item = "http://cdn.content.easports.com/fifa/fltOnlineAssets/2013/fut/items/web/{0}.json";

        public const string Home = "http://www.ea.com/uk/football/login?redirectUrl=http://www.ea.com/uk/football/fifa-ultimate-team";

        public static string FutHostName;

        public const string AuctionHouse = "/ut/game/fifa13/auctionhouse";

        public const string Search = AuctionHouse + "?start={0}&num={1}";

        public const string TradeStatus = "/ut/game/fifa13/trade?tradeIds={0}";

        public const string Bid = "/ut/game/fifa13/trade/{0}/bid";

        public const string PurchasedItems = "/ut/game/fifa13/purchased/items";

        public const string Credits = "/ut/game/fifa13/user/credits";

        public const string TradePile = "/ut/game/fifa13/item";

        public const string Watchlist = "/ut/game/fifa13/watchlist";
    }
}