using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Minecraft.Crafting.Website.Pages;

namespace Minecraft.Crafting.Website.Shared
{
    public partial class NavMenu
    {
        [Inject]
    public IStringLocalizer<List> Localizer { get; set; }
}
}
