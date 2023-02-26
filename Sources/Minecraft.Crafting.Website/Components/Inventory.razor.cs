using Microsoft.AspNetCore.Components;
using System.Collections.ObjectModel;
using Minecraft.Crafting.Website.Pages;
using Minecraft.Crafting.Website.Services;
using Blazored.LocalStorage;
using Microsoft.Extensions.Localization;

namespace Minecraft.Crafting.Website.Components
{
    public partial class Inventory
    {
        public ObservableCollection<CraftingAction> Actions { get; set; }

        [Parameter]
        public InventoryPage Parent { get; set; }

        //[Parameter]
        public List<Models.Item> Items { get; set; }

        [Inject]
        public IInventoryService InventoryService { get; set; }

        [Inject]
        public ILogger<LogModel> Logger { get; set; }

        [Inject]
        public HttpClient Http { get; set; }

        [Inject]
        public ILocalStorageService LocalStorage { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public IStringLocalizer<Add> Localizer { get; set; }
    }

}
