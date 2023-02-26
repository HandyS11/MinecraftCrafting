using Microsoft.AspNetCore.Components;
using Minecraft.Crafting.Website.Components;
using Minecraft.Crafting.Website.Factories;
using Minecraft.Crafting.Website.Models;
using Minecraft.Crafting.Website.Pages;

namespace Minecraft.Crafting.Website.Services
{
    public class DataApiService : IDataService
    {
        private readonly HttpClient _http;
        private readonly string host;
        private int lastCount = 0;

        public int getLastCount() => lastCount;

        public DataApiService(HttpClient http, IConfiguration configuration)
        {
            _http = http;
            host = configuration["API_URL"];
        }

        public async Task Add(ItemModel model)
        {
            // Get the item
            var item = ItemFactory.Create(model);

            // Save the data
            await _http.PostAsJsonAsync(host + "/api/Crafting/", item);
        }

        public async Task<int> Count()
        {
            return await _http.GetFromJsonAsync<int>(host + "/api/Crafting/count");
        }

        public async Task<List<Item>> List(int currentPage, int pageSize, bool orderByName = false)
        {
            // It should be great to implement a brand new method in the API
            // Old way with not sort: List<Item> aa = await _http.GetFromJsonAsync<List<Item>>($"{host}api/Crafting/?currentPage={currentPage}&pageSize={pageSize}");
            var itemList = await _http.GetFromJsonAsync<List<Item>>($"{host}/api/Crafting/All");
            List<Item> items = itemList.ToList();
            if (items.Count < 0)
            {
                return new List<Item>();
            }
            var result = items;
            if (orderByName)
            {
                result = items.OrderBy(item => item.DisplayName).ToList();
            }
            lastCount = result.Count;
            return result.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();
        }

        public async Task<List<Item>> ListSearch(int currentPage, int pageSize, string searchBy, bool orderByName = false)
        {
            // It should be great to implement a brand new method in the API
            var itemList = await _http.GetFromJsonAsync<List<Item>>($"{host}/api/Crafting/All");
            List<Item> itemFilterList = itemList.Where(item => item.DisplayName.ContainsIgnoreCase(searchBy)).ToList();
            if (itemFilterList.Count < 0)
            {
                return new List<Item>();
            }
            var result = itemFilterList;
            if (orderByName)
            {
                result = itemFilterList.OrderBy(item => item.DisplayName).ToList();
            }
            lastCount = result.Count;
            return result.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();
        }

        public async Task<Item> GetById(int id)
        {
            return await _http.GetFromJsonAsync<Item>($"{host}/api/Crafting/{id}");
        }

        public async Task Update(int id, ItemModel model)
        {
            // Get the item
            var item = ItemFactory.Create(model);

            await _http.PutAsJsonAsync($"{host}/api/Crafting/{id}", item);
        }

        public async Task Delete(int id)
        {
            await _http.DeleteAsync($"{host}/api/Crafting/{id}");
        }

        public async Task<List<CraftingRecipe>> GetRecipes()
        {
            return await _http.GetFromJsonAsync<List<CraftingRecipe>>(host + "/api/Crafting/recipe");
        }
    }
}
