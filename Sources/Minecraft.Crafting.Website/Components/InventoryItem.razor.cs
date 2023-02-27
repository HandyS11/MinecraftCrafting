using Blazorise;
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

        internal void OnDragEnter(DragEventArgs e)
        {
            if (!CommonParent.Dragging)
            {
                CommonParent.OnDragbegin(this);
                logger.Log(LogLevel.Information, "User has begun to drag element :" + CommonParent?.CurrentDragItem?.Item?.DisplayName);
            }
        }

        internal void OnDrop(DragEventArgs e)
        {
            if (!InventorySide || CommonParent == null || CommonParent.CurrentDragItem.Item == null)
            {
                return;
            }

            if (CommonParent != null && CommonParent.Dragging)
            {
                if (this.Item == null)
                {
                    this.Item = CommonParent.CurrentDragItem.Item;
                    if (CommonParent.CurrentDragItem.IItem == null)
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
                        inventoryService.AddToInventory(IItem);
                    }
                    logger.Log(LogLevel.Information,
                        $"User has finished dragging element :{CommonParent?.CurrentDragItem?.Item?.DisplayName} and has dropped it on cell {IItem?.Position} which was empty");
                    CommonParent.OnDragend();
                }
                else
                {
                    logger.Log(LogLevel.Information, 
                        $"User has finished dragging element :{CommonParent?.CurrentDragItem?.Item?.DisplayName} and has dropped it on cell {IItem?.Position} which contained {Item.DisplayName}");
                    CommonParent.OnDragend(false);
                }
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
            CommonParent.OnDragend();
        }

        private void OnClick(MouseEventArgs cea)
        {
            if( IItem != null && cea.Button == (long)MouseButton.Left)
            {
                IItem.NumberItem /= 2;
                inventoryService.UpdateInventory(IItem);
            }
        }

        public void empty()
        {
            if (IItem != null)
            {
                //inventoryService.AddToInventory(IItem);
                IItem.Position = Index;
                inventoryService.RemoveFromInventory(IItem);
                IItem = null;
                Item = null;
            }
        }

        public void forceRefresh()
        {
            StateHasChanged();
        }
    }
}
