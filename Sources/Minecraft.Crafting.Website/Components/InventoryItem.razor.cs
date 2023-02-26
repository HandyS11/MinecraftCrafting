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
        public bool NoDrop { get; set; }

        [Parameter]
        public InventoryPage CommonParent { get; set; }

        [Inject]
        private ILogger<LogModel> logger { get; set; }

        internal void OnDragEnter()
        {
            if (NoDrop)
            {
                return;
            }

            //Parent.Actions.Add(new CraftingAction { Action = "Drag Enter", Item = this.Item, Index = this.Index });
        }

        internal void OnDragEnter(DragEventArgs e)
        {
            if (!CommonParent.Dragging)
            {
                CommonParent.OnDragbegin(Item);
            }
            //Parent.Actions.Add(new CraftingAction { Action = "Drag Enter", Item = this.Item, Index = this.Index });
        }

        internal void OnDragLeave()
        {
            if (NoDrop)
            {
                return;
            }
           // Parent.Actions.Add(new CraftingAction { Action = "Drag Leave", Item = this.Item, Index = this.Index });
        }
        internal void OnDrop()
        {
            if (NoDrop)
            {
                return;
            }
            /*
            this.Item = Parent.CurrentDragItem;
            Parent.RecipeItems[this.Index] = this.Item;

            Parent.Actions.Add(new CraftingAction { Action = "Drop", Item = this.Item, Index = this.Index });

            // Check recipe
            Parent.CheckRecipe();*/
        }

        internal void OnDrop(DragEventArgs e)
        {
            if (NoDrop)
            {
                return;
            }
            if (CommonParent != null && CommonParent.Dragging)
            {
                this.Item = CommonParent.CurrentDragItem;
                CommonParent.OnDragend();
            }
            else
                logger.Log(LogLevel.Error, "parent was null for InventoryItem!");
        }

        private void OnDragStart()
        {
            CommonParent.OnDragbegin(Item);
            /*
            Parent.CurrentDragItem = this.Item;

            Parent.Actions.Add(new CraftingAction { Action = "Drag Start", Item = this.Item, Index = this.Index });*/
        }
    }
}
