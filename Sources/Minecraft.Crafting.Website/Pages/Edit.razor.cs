using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.Extensions.Localization;
using Minecraft.Crafting.Website.Factories;
using Minecraft.Crafting.Website.Models;
using Minecraft.Crafting.Website.Services;

namespace Minecraft.Crafting.Website.Pages
{
    public partial class Edit
    {
        [Inject]
        public IStringLocalizer<Edit> Localizer { get; set; }

        [Parameter]
        public int Id { get; set; }

        /// <summary>
        /// The default enchant categories.
        /// </summary>
        private List<string> enchantCategories = new List<string>() { "armor", "armor_head", "armor_chest", "weapon", "digger", "breakable", "vanishable" };

        /// <summary>
        /// The current item model
        /// </summary>
        private ItemModel itemModel = new()
        {
            EnchantCategories = new List<string>(),
            RepairWith = new List<string>()
        };

        /// <summary>
        /// The default repair with.
        /// </summary>
        private List<string> repairWith = new List<string>() { "oak_planks", "spruce_planks", "birch_planks", "jungle_planks", "acacia_planks", "dark_oak_planks", "crimson_planks", "warped_planks" };

        [Inject]
        public IDataService DataService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public IWebHostEnvironment WebHostEnvironment { get; set; }

        [Inject]
        public ILogger<LogModel> Logger { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Logger.Log(LogLevel.Information, "Edit OnInitializedAsync");
            var item = await DataService.GetById(Id);
            Logger.Log(LogLevel.Information, "Edit OnInitializedAsync got item: "+item.DisplayName, item);
            var fileContent = await File.ReadAllBytesAsync($"{WebHostEnvironment.WebRootPath}/images/default.png");

            // Set the model with the item
            itemModel = ItemFactory.ToModel(item, fileContent);
        }

        private async void HandleValidSubmit()
        {
            Logger.Log(LogLevel.Information, "Edit HandleValidSubmit");
            await DataService.Update(Id, itemModel);
            Logger.Log(LogLevel.Information, "Edit HandleValidSubmit done, navigating to 'list'");
            NavigationManager.NavigateTo("list");
        }

        private async Task LoadImage(InputFileChangeEventArgs e)
        {
            Logger.Log(LogLevel.Information, "Edit LoadImage");
            // Set the content of the image to the model
            using (var memoryStream = new MemoryStream())
            {
                await e.File.OpenReadStream().CopyToAsync(memoryStream);
                itemModel.ImageContent = memoryStream.ToArray();
            }
        }

        private void OnEnchantCategoriesChange(string item, object checkedValue)
        {
            Logger.Log(LogLevel.Information, "Edit OnEnchantCategoriesChange");
            if ((bool)checkedValue)
            {
                if (!itemModel.EnchantCategories.Contains(item))
                {
                    itemModel.EnchantCategories.Add(item);
                }

                return;
            }

            if (itemModel.EnchantCategories.Contains(item))
            {
                itemModel.EnchantCategories.Remove(item);
            }
        }

        private void OnRepairWithChange(string item, object checkedValue)
        {
            Logger.Log(LogLevel.Information, "Edit OnRepairWithChange");
            if ((bool)checkedValue)
            {
                if (!itemModel.RepairWith.Contains(item))
                {
                    itemModel.RepairWith.Add(item);
                }

                return;
            }

            if (itemModel.RepairWith.Contains(item))
            {
                itemModel.RepairWith.Remove(item);
            }
        }
    }
}
