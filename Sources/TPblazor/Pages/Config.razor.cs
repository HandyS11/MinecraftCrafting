using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Options;
using TPblazor.Models;

namespace TPblazor.Pages
{
    public partial class Config
    {
        [Inject]
        public IConfiguration Configuration { get; set; }

        [Inject]
        public IOptions<PositionOptions> OptionsPositionOptions { get; set; }

        private PositionOptions positionOptions;

        protected override void OnInitialized()
        {
            base.OnInitialized();

            positionOptions = OptionsPositionOptions.Value;
        }
    }
}
