using frontend.Models;

namespace frontend.Services;

public interface IItemService
{
    Task<Item> GetItemAsync(int itemId);
}