using Microsoft.AspNetCore.Components;
using Minecraft.Crafting.Website.Components;
using Minecraft.Crafting.Website.Models;
using Minecraft.Crafting.Website.Services;

namespace Minecraft.Crafting.Website.Pages
{
    public partial class Index
    {
        [Inject]
        public ILogger<LogModel> Logger { get; set; }

        [Inject]
        public IDataService DataService { get; set; }

        public List<Item> Items { get; set; } = new List<Item>();

        private List<CraftingRecipe> Recipes { get; set; } = new List<CraftingRecipe>();

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            base.OnAfterRenderAsync(firstRender);
            Logger.Log(LogLevel.Information, "OnAfterRenderAsync", null);

            if (!firstRender)
            {
                return;
            }

            Items = await DataService.List(0, await DataService.Count());
            Recipes = await DataService.GetRecipes();

            StateHasChanged();
        }
    }
}
