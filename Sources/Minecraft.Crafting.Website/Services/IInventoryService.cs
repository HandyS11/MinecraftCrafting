using Minecraft.Crafting.Website.Models;

namespace Minecraft.Crafting.Website.Services
{
    public interface IInventoryService
    {
        Task AddToInventory(Item i);
        Task RemoveFromInventory(Item i);
        Task<List<Item>> GetAll();
        Task UpdateInveotry(Item i);
    }
}
