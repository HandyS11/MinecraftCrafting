using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
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

        public Item CurrentDragItem { get; private set; }
        public Boolean Dragging { get; private set; } = false;

        public void OnDragbegin(Item i)
        {
            CurrentDragItem = i;
            Dragging= true;
        }

        public void OnDragend()
        {
            CurrentDragItem = null;
            Dragging= false;
        }
    }
}
