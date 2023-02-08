﻿using Microsoft.AspNetCore.Components;
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

        private PositionOptions positionOptions;

        protected override void OnInitialized()
        {
            base.OnInitialized();

            positionOptions = OptionsPositionOptions.Value;
        }
    }
}