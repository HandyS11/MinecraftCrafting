using Minecraft.Crafting.Api.Models;
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

        public async Task AddToInventory(InventoryModel i)
        {

            // Save the data
            await _http.PostAsJsonAsync(host + "api/Inventory/", i);
        }

        public async Task<List<InventoryModel>> GetAll()
        {
            List<InventoryModel> aa = await _http.GetFromJsonAsync<List<InventoryModel>>($"{host}api/Inventory/");
            return aa;
        }

        public async Task RemoveFromInventory(InventoryModel i)
        {
            await _http.DeleteAsync($"{host}api/Inventory/");
        }

        public async Task UpdateInventory(InventoryModel i)
        {

            await _http.PutAsJsonAsync($"{host}api/Inventory/", i);
        }
    }
}
