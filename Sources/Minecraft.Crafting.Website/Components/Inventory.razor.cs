using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using Minecraft.Crafting.Website.Models;

namespace Minecraft.Crafting.Website.Components
{
        public partial class Inventory
        {

            public Inventory()
            {
                Actions = new ObservableCollection<CraftingAction>();
                Actions.CollectionChanged += OnActionsCollectionChanged;
            }

            public ObservableCollection<CraftingAction> Actions { get; set; }
            public Item CurrentDragItem { get; set; }

            [Parameter]
            public List<Item> Items { get; set; }

            /// <summary>
            /// Gets or sets the java script runtime.
            /// </summary>
            [Inject]
            internal IJSRuntime JavaScriptRuntime { get; set; }

            private void OnActionsCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
            {
                JavaScriptRuntime.InvokeVoidAsync("Crafting.AddActions", e.NewItems);
            }
       }

}
