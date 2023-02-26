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

    }
}
