using Minecraft.Crafting.Api.Models;
using Minecraft.Crafting.Website.Factories;
using Minecraft.Crafting.Website.Models;
using Newtonsoft.Json;
using System.Text;

namespace Minecraft.Crafting.Website.Services
{
    public class InventoryApiService : IInventoryService
    {
        private readonly HttpClient _http;
        private readonly string host;

        public InventoryApiService(HttpClient http, IConfiguration configuration)
        {
            _http = http;
            host = configuration["API_URL"];
        }

        public async Task AddToInventory(InventoryModel i)
        {

            // Save the data
            await _http.PostAsJsonAsync(host + "/api/Inventory/", i);
        }

        public async Task<List<InventoryModel>> GetAll()
        {
            List<InventoryModel> aa = await _http.GetFromJsonAsync<List<InventoryModel>>($"{host}/api/Inventory/");
            return aa;
        }

        public async Task RemoveFromInventory(InventoryModel i)
        {
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Delete,
                RequestUri = new Uri($"{host}/api/Inventory/"),
                Content = new StringContent(JsonConvert.SerializeObject(i), Encoding.UTF8, "application/json")
            };
            var response = await _http.SendAsync(request);
        }

        public async Task UpdateInventory(InventoryModel i)
        {
            await _http.PutAsJsonAsync($"{host}/api/Inventory/", i);
        }
    }
}
