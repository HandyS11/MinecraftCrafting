using Minecraft.Crafting.Website.Factories;
using Minecraft.Crafting.Website.Models;

namespace Minecraft.Crafting.Website.Services
{
    public class InventoryApiService : IInventoryService
    {
        private readonly HttpClient _http;
        private readonly string host;

        public InventoryApiService(HttpClient http, IConfiguration configuration)
        {
            _http = http;
            host = configuration["HOST_URL"];
        }

        public async Task AddToInventory(ItemModel i)
        {
            // Get the item
            var item = ItemFactory.Create(i);

            // Save the data
            await _http.PostAsJsonAsync(host + "api/Invenotry/", item);
        }

        public async Task<List<Item>> GetAll()
        {
            List<Item> aa = await _http.GetFromJsonAsync<List<Item>>($"{host}api/Inventory/");
            return aa;
        }

        public async Task RemoveFromInventory(ItemModel i)
        {
            await _http.DeleteAsync($"{host}api/Inventory/");
        }

        public async Task UpdateInveotry(ItemModel i)
        {
            var item = ItemFactory.Create(i);

            await _http.PutAsJsonAsync($"{host}api/Crafting/", item);
        }
    }
}
