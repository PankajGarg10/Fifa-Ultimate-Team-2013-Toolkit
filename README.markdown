## Sample usage

[Login](https://github.com/trydis/Fifa-Ultimate-Team-2013-Toolkit#login)  
[Player search](https://github.com/trydis/Fifa-Ultimate-Team-2013-Toolkit#player-search)  
[Trade status](https://github.com/trydis/Fifa-Ultimate-Team-2013-Toolkit#trade-status)  
[Place bids](https://github.com/trydis/Fifa-Ultimate-Team-2013-Toolkit#place-bids)  
[Player image](https://github.com/trydis/Fifa-Ultimate-Team-2013-Toolkit#player-image)  
[Item data](https://github.com/trydis/Fifa-Ultimate-Team-2013-Toolkit#item-data)  
[Purchased items](https://github.com/trydis/Fifa-Ultimate-Team-2013-Toolkit#purchased-items)  
[Credits](https://github.com/trydis/Fifa-Ultimate-Team-2013-Toolkit#credits)  
[Send to trade pile](https://github.com/trydis/Fifa-Ultimate-Team-2013-Toolkit#send-to-trade-pile)  
[Available parameter values](https://github.com/trydis/Fifa-Ultimate-Team-2013-Toolkit#available-parameter-values)  
[Extension methods](https://github.com/trydis/Fifa-Ultimate-Team-2013-Toolkit#extension-methods)  

### Login

```csharp
var loginRequest = new LoginRequest();
await loginRequest.LoginAsync("e-mail", "password", "secret answer");
```

### Player search

All the search parameters are optional. If none are specified, you will get the 1st page of results with no filters applied.

```csharp
var searchRequest = new SearchRequest();
var searchParameters = new PlayerSearchParameters
{
    Page = 1,
    Level = Level.Gold,
    Formation = Formation.FourThreeThree,
    League = League.BarclaysPremierLeague,
    Nation = Nation.Norway,
    Position = Position.Striker,
    Team = Team.ManchesterUnited
};

var searchResponse = await searchRequest.SearchAsync(searchParameters);
foreach (var auctionInfo in searchResponse.AuctionInfo)
{
	// Handle auction data
}
```

### Trade status

Retrieves the trade statuses of the auctions of interest.

```csharp
var tradeRequest = new TradeRequest();
var auctionResponse = await tradeRequest.GetTradeStatuses(
    Auctions // Contains the auctions we're currently watching
    .Where(model => model.AuctionInfo.Expires != -1)
    .Select(model => model.AuctionInfo.TradeId));

foreach (var auctionInfo in auctionResponse.AuctionInfo)
{
	// Handle the update auction data
}
```

### Place bids

Passing the amount explicitly:

```csharp
var bidRequest = new BidRequest();
var bidResponse = await bidRequest.PlaceBid(auctionInfo, 100);
```

Place the next valid bid amount:

```csharp
var bidRequest = new BidRequest();
var bidResponse = await bidRequest.PlaceBid(auctionInfo, auctionInfo.CalculateBid());
```

### Player image

- Format: PNG
- Dimensions: 100 x 100 pixels

```csharp
var playerImageRequest = new PlayerImageRequest();
var imageBytes = await playerImageRequest.GetImageAsync(auctionInfo.ItemData.ResourceId);
```

### Item data

Contains info such as name, ratings etc.

```csharp
var itemRequest = new ItemRequest();
var item = await itemRequest.GetItemAsync(auctionInfo.ItemData.ResourceId);
```

### Purchased items

Items that have been bought or received in gift packs.

```csharp
var purchasedItemsRequest = new PurchasedItemsRequest();
var purchasedItemsResponse = await purchasedItemsRequest.GetPurchasedItemsAsync();
foreach (var itemData in purchasedItemsResponse.ItemData)
{
    
}
```

### Credits

Amount of coins and unopened packs.

```csharp
var creditsRequest = new CreditsRequest();
var creditsResponse = await creditsRequest.GetCreditsAsync();
```

### Send to trade pile

Sends an item to the trade pile.

```csharp
var tradePileRequest = new TradePileRequest();
var tradePileResponse = await tradePileRequest.SendToTradePileAsync(itemData);
```

### Available parameter values

Handy if you want to present all the options in the client application, for instance in a drop-down list.

Mapping all these values will be an ongoing process, wouldn't mind some pull requests as help.

**Levels**

This is an enum, use as is.

**Formations**
```csharp
IEnumerable<Formation> formations = Formation.GetAll();
```

**Leagues**
```csharp
IEnumerable<League> leagues = League.GetAll();
```

**Nations**
```csharp
IEnumerable<Nation> nations = Nation.GetAll();
```

**Positions**
```csharp
IEnumerable<Position> positions = Position.GetAll();
```

**Teams**
```csharp
IEnumerable<Team> teams = Team.GetAll();
```

### Extension methods

Automatically calculates the next valid bid amount:

```csharp
auctionInfo.CalculateBid();
```

Handy if you want to download the player images, item data etc. from the EA servers. The ItemRequest class uses this method internally.

```csharp
auctionInfo.ItemData.ResourceId.CalculateBaseId();
```