using frontend.Models;
using Newtonsoft.Json;
using Microsoft.Extensions.Caching.Distributed;

namespace frontend.Services;

public class ItemService : IItemService
{
    private readonly IDistributedCache _cache;
    private readonly TimeSpan _cacheExpiry = TimeSpan.FromMinutes(5);

    public ItemService(IDistributedCache cache)
    {
        _cache = cache;
    }

    public async Task<Item> GetItemAsync(int itemId)
    {
        string cacheKey = $"Item_{itemId}";
        
        // Step 1: Try to get the item from Redis cache
        var cachedItem = await _cache.GetStringAsync(cacheKey);
        if (cachedItem != null)
        {
            Console.WriteLine("✅ Item retrieved from cache!");
            return JsonConvert.DeserializeObject<Item>(cachedItem);
        }

        // Step 2: Simulate database fetch (or real DB call in a production app)
        var item = await FetchItemFromDatabase(itemId);

        // Step 3: Cache the item in Redis
        await _cache.SetStringAsync(
            cacheKey, 
            JsonConvert.SerializeObject(item),
            new DistributedCacheEntryOptions { AbsoluteExpirationRelativeToNow = _cacheExpiry }
        );

        Console.WriteLine("🔄 Item added to cache.");
        return item;
    }

    private Task<Item> FetchItemFromDatabase(int itemId)
    {
        // Simulating a database call
        return Task.FromResult(new Item { Id = itemId, Name = "Sample Item", Description = "A cached item." });
    }
}