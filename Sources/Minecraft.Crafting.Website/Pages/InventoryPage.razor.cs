using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Minecraft.Crafting.Website.Components;
using Minecraft.Crafting.Website.Models;
using Minecraft.Crafting.Website.Services;

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
        public InventoryItem CurrentDragItem { get; private set; }
        public List<Item> items { get; private set; }
        public bool Dragging { get; private set; } = false;

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
