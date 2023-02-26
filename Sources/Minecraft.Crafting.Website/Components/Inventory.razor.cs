using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using Minecraft.Crafting.Website.Models;
using Microsoft.Extensions.Logging;
using static System.Net.WebRequestMethods;
using Minecraft.Crafting.Website.Pages;
using Minecraft.Crafting.Website.Services;
using Blazored.LocalStorage;

namespace Minecraft.Crafting.Website.Components
{
        public partial class Inventory
        {

            public Inventory()
            {
            }

            public ObservableCollection<CraftingAction> Actions { get; set; }

            [Parameter]
            public InventoryPage Parent { get; set; }

            //[Parameter]
            public List<Website.Models.Item> Items { get; set; }

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
       }

}
