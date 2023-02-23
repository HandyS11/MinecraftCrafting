using Blazored.Modal.Services;
using Blazored.Modal;
using Microsoft.AspNetCore.Components;
using Minecraft.Crafting.Website.Models;
using Minecraft.Crafting.Website.Services;
using Microsoft.Extensions.Localization;

namespace Minecraft.Crafting.Website.Modals
{
    public partial class DeleteConfirmation
    {
        [Inject]
        public IStringLocalizer<DeleteConfirmation> Localizer { get; set; }

        [CascadingParameter]
        public BlazoredModalInstance ModalInstance { get; set; }

        [Inject]
        public IDataService DataService { get; set; }

        [Parameter]
        public int Id { get; set; }

        private Item item = new Item();

        protected override async Task OnInitializedAsync()
        {
            // Get the item
            item = await DataService.GetById(Id);
        }

        void ConfirmDelete()
        {
            ModalInstance.CloseAsync(ModalResult.Ok(true));
        }

        void Cancel()
        {
            ModalInstance.CancelAsync();
        }
    }
}
