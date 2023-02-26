using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using System.Globalization;

namespace Minecraft.Crafting.Website.Shared
{
    public partial class CultureSelector
    {
        [Inject]
        public IStringLocalizer<CultureSelector> Localizer { get; set; }

        private readonly CultureInfo[] supportedCultures = new[]
        {
            new CultureInfo("en-US"),
            new CultureInfo("fr-FR")
        };

        private CultureInfo Culture
        {
            get => CultureInfo.CurrentCulture;
            set
            {
                if (CultureInfo.CurrentUICulture == value)
                {
                    return;
                }

                var culture = value.Name.ToLower(CultureInfo.InvariantCulture);

                var uri = new Uri(this.NavigationManager.Uri).GetComponents(UriComponents.PathAndQuery, UriFormat.Unescaped);
                var query = $"?culture={Uri.EscapeDataString(culture)}&" + $"redirectUri={Uri.EscapeDataString(uri)}";

                // Redirect the user to the culture controller to set the cookie
                this.NavigationManager.NavigateTo("/Culture/SetCulture" + query, forceLoad: true);
            }
        }
    }
}
