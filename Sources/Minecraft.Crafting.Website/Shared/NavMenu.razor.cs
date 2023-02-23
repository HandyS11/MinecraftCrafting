using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;

namespace Minecraft.Crafting.Website.Shared
{
    public partial class NavMenu
    {
        [Inject]
        public IStringLocalizer<NavMenu> Localizer { get; set; }

        private bool collapseNavMenu = true;

        private string? NavMenuCssClass => collapseNavMenu ? "collapse" : null;

        private void ToggleNavMenu()
        {
            collapseNavMenu = !collapseNavMenu;
        }
    }
}
