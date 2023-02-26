using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Minecraft.Crafting.Api.Models;
using Minecraft.Crafting.Website.Components;
using Minecraft.Crafting.Website.Models;
using Minecraft.Crafting.Website.Services;
using static System.Net.WebRequestMethods;

namespace Minecraft.Crafting.Website.Pages
{
    public partial class InventoryPage
    {

        [Inject]
        public IStringLocalizer<List> Localizer { get; set; }
        [Inject]
        public ILogger<LogModel> Logger { get; set; }
        [Inject]
        public IInventoryService InventoryService { get; set; }
        [Inject]
        public IDataService cs { get; set; }
        public InventoryItem CurrentDragItem { get; private set; }
        public Dictionary<int, InventoryItem> uiItems { get; set; } = new Dictionary<int, InventoryItem>();
        public bool Dragging { get; private set; } = false;

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            // Do not treat this action if is not the first render
            if (!firstRender)
            {
                return;
            }

            List<InventoryModel> le = await InventoryService.GetAll();
            List<Website.Models.Item> items = await cs.List(0, 999);
            foreach(var l in le)
            {
                uiItems[l.Position].IItem = l;
                uiItems[l.Position].Item = items.First(it => it.Name == l.ItemName);
                uiItems[l.Position].forceRefresh();
            }
        }

        public void OnDragbegin(InventoryItem i)
        {
            CurrentDragItem = i;
            Dragging= true;
        }

        public async void OnDragend()
        {
            //await InventoryService.AddToInventory(CurrentDragItem);
            CurrentDragItem?.empty();
            CurrentDragItem = null;
            Dragging= false;
        }
    }
}
