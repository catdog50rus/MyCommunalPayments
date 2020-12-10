using Microsoft.AspNetCore.Components;
using MyCommunalPayments.Data.Services.Toast;

namespace MyCommunalPayments.BlazorWebUI.Components
{
    public class ToastBase : ComponentBase
    {
        [Inject] 
        public IToast ToastService { get; set; }

        [Parameter]
        public RenderFragment Message { get; set; }

        [Parameter]
        public EventCallback OnClickConfirm { get; set; }

        protected bool IsVisible;
        protected string BackgroundCssClass;
        protected string IconCssClass;
        protected string Heading;
        protected string windowShow = "";
        protected bool isWarning;

        protected bool isConfirm = false;

        protected override void OnInitialized()
        {
            HideToast();
        }

        public void ShowToast(ToastLevel level)
        {
            var toast = ToastService.ShowToast(level);
            BackgroundCssClass = toast.Item1;
            IconCssClass = toast.Item2;
            Heading = toast.Item3;
            isWarning = Heading == ToastLevel.Warning.ToString();
            IsVisible = true;
            windowShow = "show";
        }

        protected void HideToast()
        {
            IsVisible = default;
            BackgroundCssClass = "";
            IconCssClass = "";
            windowShow = "hide";
            if (isConfirm)
            {
                OnClickConfirm.InvokeAsync(isConfirm);
            }
        }
    }
}
