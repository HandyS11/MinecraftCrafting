using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Minecraft.Crafting.Website.Models;
using Minecraft.Crafting.Website.Pages;

namespace Minecraft.Crafting.Website.Components
{
    public partial class InventoryItem
    {
        [Parameter]
        public int Index { get; set; }

        [Parameter]
        public Item Item { get; set; }

        [Parameter]
        public bool InventorySide { get; set; }

        [Parameter]
        public InventoryPage CommonParent { get; set; }

        [Inject]
        private ILogger<LogModel> logger { get; set; }

        internal void OnDragEnter()
        {
            if (!InventorySide)
            {
                return;
            }
        }

        internal void OnDragEnter(DragEventArgs e)
        {
            if (!CommonParent.Dragging)
            {
                CommonParent.OnDragbegin(this);
            }
        }

        internal void OnDragLeave()
        {
            if (!InventorySide)
            {
                return;
            }
        }
        internal void OnDrop()
        {
            if (!InventorySide)
            {
                return;
            }
        }

        internal void OnDrop(DragEventArgs e)
        {
            if (!InventorySide)
            {
                return;
            }
            if (CommonParent != null && CommonParent.Dragging)
            {
                this.Item = CommonParent.CurrentDragItem.Item;
                CommonParent.OnDragend();
            }
            else
                logger.Log(LogLevel.Error, "parent was null for InventoryItem!");
        }

        private void OnDragStart()
        {
            CommonParent.OnDragbegin(this);
        }

        private void OnDragEnd()
        {
            if (!InventorySide)
            {
                return;
            }
            CommonParent.OnDragend();
        }

        public void empty()
        {
            Item = null;
        }
    }
}
