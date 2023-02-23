using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Minecraft.Crafting.Website.Models;
using Minecraft.Crafting.Website.Services;

namespace Minecraft.Crafting.Website.Pages
{
    public partial class InventoryPage
    {
        [Inject]
        public IDataService DataService { get; set; }

        [Inject]
        public IStringLocalizer<List> Localizer { get; set; }
        [Inject]
        public ILogger<LogModel> Logger { get; set; }

        public List<Item> Items { get; set; } = new List<Item>();


        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            _ = base.OnAfterRenderAsync(firstRender);
            Logger.Log(LogLevel.Information, "InventoryPage OnAfterRenderAsync", firstRender);

            if (!firstRender)
            {
                return;
            }

            Items = await DataService.List(0, await DataService.Count());

            Logger.Log(LogLevel.Information, "InventoryPage OnAfterRenderAsync loaded items: "+Items.Count, Items);

            StateHasChanged();
        }

    }
}
