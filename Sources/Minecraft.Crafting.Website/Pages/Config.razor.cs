using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Minecraft.Crafting.Website.Models;

namespace Minecraft.Crafting.Website.Pages
{
    public partial class Config
    {
        [Inject]
        public IConfiguration Configuration { get; set; }

        [Inject]
        public IOptions<PositionOptions> OptionsPositionOptions { get; set; }
        [Inject]
        public ILogger<LogModel> Logger { get; set; }

        private PositionOptions positionOptions;

        protected override void OnInitialized()
        {
            base.OnInitialized();
            Logger.Log(LogLevel.Information, "Config OnInitialized");
            positionOptions = OptionsPositionOptions.Value;
        }
    }
}
