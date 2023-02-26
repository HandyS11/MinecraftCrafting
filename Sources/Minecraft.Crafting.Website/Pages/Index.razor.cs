using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Localization;
using Minecraft.Crafting.Website.Components;
using Minecraft.Crafting.Website.Models;
using Minecraft.Crafting.Website.Services;

namespace Minecraft.Crafting.Website.Pages
{
    public partial class Index
    {
        [Inject]
        public IStringLocalizer<Index> Localizer { get; set; }

        [Inject]
        public IDataService DataService { get; set; }

        [Inject]
        public ILogger<LogModel> lm { get; set; }

        [Inject]
        public IConfiguration configuration { get; set; }

        public List<Item> Items { get; set; } = new List<Item>();

        private List<CraftingRecipe> Recipes { get; set; } = new List<CraftingRecipe>();

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            base.OnAfterRenderAsync(firstRender);
            lm?.Log(LogLevel.Information, "now running Index on host: " + configuration["API_URL"]);

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
