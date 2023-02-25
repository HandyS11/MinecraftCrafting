using Minecraft.Crafting.Website.Components;
using Minecraft.Crafting.Website.Models;

namespace Minecraft.Crafting.Website.Services
{
    public interface IDataService
    {
        Task Add(ItemModel model);

        Task<int> Count();

        public int getLastCount();

        Task<List<Item>> List(int currentPage, int pageSize);

        Task<List<Item>> ListSearch(int currentPage, int pageSize, string searchBy);

        Task<Item> GetById(int id);

        Task Update(int id, ItemModel model);

        Task Delete(int id);

        Task<List<CraftingRecipe>> GetRecipes();
    }
}
