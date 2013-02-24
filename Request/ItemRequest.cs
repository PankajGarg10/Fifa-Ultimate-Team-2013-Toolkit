using System;
using System.Threading.Tasks;
using UltimateTeam.Toolkit.Extension;
using UltimateTeam.Toolkit.Model;

namespace UltimateTeam.Toolkit.Request
{
    public class ItemRequest : RequestBase
    {
        public async Task<Item> GetItemAsync(long resourceId)
        {
            var baseId = resourceId.CalculateBaseId();
            var uriString = string.Format(Resources.Item, baseId);
            var uri = new Uri(uriString);

            var stream = await Client.GetStreamAsync(uri);
            
            var itemWrapper = JsonDeserializer.Deserialize<ItemWrapper>(stream);

            return itemWrapper.Item;
        }
    }
}