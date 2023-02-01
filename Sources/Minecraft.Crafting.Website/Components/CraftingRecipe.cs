using Minecraft.Crafting.Website.Models;

namespace Minecraft.Crafting.Website.Components
{
    public class CraftingRecipe
    {
        public Item Give { get; set; }
        public List<List<string>> Have { get; set; }
    }
}
