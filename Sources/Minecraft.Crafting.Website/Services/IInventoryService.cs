using Minecraft.Crafting.Api.Models;
using Minecraft.Crafting.Website.Models;

namespace Minecraft.Crafting.Website.Services
{
    public interface IInventoryService
    {
        Task AddToInventory(InventoryModel i);
        Task RemoveFromInventory(InventoryModel i);
        Task<List<InventoryModel>> GetAll();
        Task UpdateInventory(InventoryModel i);
    }
}
