using Minecraft.Crafting.Website.Models;

namespace Minecraft.Crafting.Website.Services
{
    public interface IInventoryService
    {
        Task AddToInventory(ItemModel i);
        Task RemoveFromInventory(ItemModel i);
        Task<List<Item>> GetAll();
        Task UpdateInveotry(ItemModel i);
    }
}
