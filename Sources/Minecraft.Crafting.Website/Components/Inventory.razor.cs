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
            public List<Item> Items { get; set; }

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

        protected override async Task OnAfterRenderAsync(bool firstRender)
            {
                // Do not treat this action if is not the first render
                if (!firstRender)
                {
                    return;
                }

                var currentData = await InventoryService.GetAll();
                // Check if data exist in the local storage
                if (currentData == null)
                {
                    Logger.Log(LogLevel.Information, "OnAfterRenderAsync currentData was null (excpected)", currentData);
                    // this code add in the local storage the fake data (we load the data sync for initialize the data before load the OnReadData method)
                    var originalData = Http.GetFromJsonAsync<Item[]>($"{NavigationManager.BaseUri}fake-data.json").Result;
                    await LocalStorage.SetItemAsync("data", originalData);
                }
                Items = currentData;
                Logger.Log(LogLevel.Information, "OnAfterRenderAsync got data: " + currentData.Count, currentData);
            }
       }

}
