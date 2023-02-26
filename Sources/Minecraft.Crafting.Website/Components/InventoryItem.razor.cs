using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Minecraft.Crafting.Api.Models;
using Minecraft.Crafting.Website.Models;
using Minecraft.Crafting.Website.Pages;
using Minecraft.Crafting.Website.Services;

namespace Minecraft.Crafting.Website.Components
{
    public partial class InventoryItem
    {
        [Parameter]
        public int Index { get; set; }

        [Parameter]
        public Models.Item Item { get; set; }
        public InventoryModel IItem { get; set; }

        [Parameter]
        public bool InventorySide { get; set; }

        [Parameter]
        public InventoryPage CommonParent { get; set; }

        [Parameter]
        public Object ByPassRegister { set
            {
                CommonParent.uiItems[Index] = this;
            } 
        }

        [Inject]
        private ILogger<LogModel> logger { get; set; }
        [Inject]
        private IInventoryService inventoryService { get; set; }

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
                if(CommonParent.CurrentDragItem.IItem == null)
                {
                    IItem = new InventoryModel();
                    IItem.ItemName = Item.Name;
                    IItem.NumberItem = Item.StackSize;
                    IItem.Position = this.Index;
                    inventoryService.AddToInventory(IItem);
                }
                else
                {
                    IItem = CommonParent.CurrentDragItem.IItem;
                    IItem.Position = this.Index;
                    inventoryService.UpdateInventory(IItem);
                }

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
            if (!InventorySide || CommonParent.CurrentDragItem == null || CommonParent.CurrentDragItem.IItem == null)
            {
                return;
            }
            inventoryService.RemoveFromInventory(CommonParent.CurrentDragItem.IItem);
            CommonParent.OnDragend();
        }

        public void empty()
        {
            Item = null;
        }

        public void forceRefresh()
        {
            StateHasChanged();
        }
    }
}
