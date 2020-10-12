using System;
using System.Timers;

namespace MyCommunalPayments.Data.Services.Toast
{
    public class ToastService : IToast
    {
        public string BackgroundCssClass { get; set; }
        public string IconCssClass { get; set; }
        public string Heading { get; set; }

        public (string,string,string) ShowToast(ToastLevel level)
        {
            BuildToastSettings(level);
            return (BackgroundCssClass, IconCssClass, Heading);
        }

        private void BuildToastSettings(ToastLevel level)
        {
            switch (level)
            {
                case ToastLevel.Info:
                    BackgroundCssClass = "bg-info";
                    IconCssClass = "info";
                    Heading = "Info";
                    break;
                case ToastLevel.Success:
                    BackgroundCssClass = "bg-success";
                    IconCssClass = "check";
                    Heading = "Success";
                    break;
                case ToastLevel.Warning:
                    BackgroundCssClass = "bg-warning";
                    IconCssClass = "exclamation";
                    Heading = "Warning";
                    break;
                case ToastLevel.Error:
                    BackgroundCssClass = "bg-danger";
                    IconCssClass = "times";
                    Heading = "Error";
                    break;
            }
        }

    }

    public enum ToastLevel
    {
        Info,
        Success,
        Warning,
        Error
    }
}
