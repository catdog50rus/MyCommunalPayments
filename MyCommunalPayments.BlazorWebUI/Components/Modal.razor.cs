using Microsoft.AspNetCore.Components;

namespace MyCommunalPayments.BlazorWebUI.Components
{
    public class ModalBase : ComponentBase
    {
        [Parameter]
        public RenderFragment Title { get; set; }

        [Parameter]
        public RenderFragment Body { get; set; }

        [Parameter]
        public EventCallback CloseWindow { get; set; }

        public string ModalSize { get; set; }

        protected string modalDisplay = "none;";
        protected string modalClass = "";
        protected bool showBackdrop = false;

        public void Open()
        {
            modalDisplay = "block;";
            modalClass = "show";
            showBackdrop = true;
        }

        public void Close()
        {
            modalDisplay = "none";
            modalClass = "";
            showBackdrop = false;
        }
    }
}
