using Blazored.LocalStorage;
using Blazored.Modal;
using Blazored.Modal.Services;
using Blazorise.DataGrid;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Minecraft.Crafting.Website.Modals;
using Minecraft.Crafting.Website.Models;
using Minecraft.Crafting.Website.Services;

namespace Minecraft.Crafting.Website.Pages
{
    public partial class List
    {
        private List<Item> items;

        private int totalItem;

        [Inject]
        public IStringLocalizer<List> Localizer { get; set; }

        [Inject]
        public HttpClient Http { get; set; }

        [Inject]
        public ILocalStorageService LocalStorage { get; set; }

        [Inject]
        public IWebHostEnvironment WebHostEnvironment { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public IDataService DataService { get; set; }
        [Inject]
        public ILogger<LogModel> Logger { get; set; }

        [CascadingParameter]
        public IModalService Modal { get; set; }

        private async void OnDelete(int id)
        {
            Logger.Log(LogLevel.Information, "OnDelete", id);
            var parameters = new ModalParameters
            {
                { nameof(Item.Id), id }
            };

            var modal = Modal.Show<DeleteConfirmation>("Delete Confirmation", parameters);
            var result = await modal.Result;

            if (result.Cancelled)
            {
                Logger.Log(LogLevel.Information, "OnDelete cancelled", id);
                return;
            }

            Logger.Log(LogLevel.Information, "OnDelete confirmed", id);
            await DataService.Delete(id);
            Logger.Log(LogLevel.Information, "OnDelete Deleted, navigating to List...", id);
            // Reload the page
            NavigationManager.NavigateTo("list", true);
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            // Do not treat this action if is not the first render
            if (!firstRender)
            {
                return;
            }

            var currentData = await LocalStorage.GetItemAsync<Item[]>("data");

            // Check if data exist in the local storage
            if (currentData == null)
            {
                Logger.Log(LogLevel.Information, "OnAfterRenderAsync currentData was null (excpected)", currentData);
                // this code add in the local storage the fake data (we load the data sync for initialize the data before load the OnReadData method)
                var originalData = Http.GetFromJsonAsync<Item[]>($"{NavigationManager.BaseUri}fake-data.json").Result;
                await LocalStorage.SetItemAsync("data", originalData);
            }
            Logger.Log(LogLevel.Information, "OnAfterRenderAsync got data: " + currentData.Length, currentData);
        }

        private async Task OnReadData(DataGridReadDataEventArgs<Item> e)
        {
            if (e.CancellationToken.IsCancellationRequested)
            {
                Logger.Log(LogLevel.Information, "OnReadData CancellationRequested");
                return;
            }

            if (!e.CancellationToken.IsCancellationRequested)
            {
                Logger.Log(LogLevel.Information, "OnReadData NOT CancellationRequested");
                items = await DataService.List(e.Page, e.PageSize);
                totalItem = await DataService.Count();
                Logger.Log(LogLevel.Information, "OnReadData items loaded: "+totalItem, totalItem);
            }
        }
    }
}
