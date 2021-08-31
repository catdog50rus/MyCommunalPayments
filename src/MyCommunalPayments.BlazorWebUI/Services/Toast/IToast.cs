

namespace MyCommunalPayments.BlazorWebUI.Services.Toast
{
    public interface IToast
    {
        (string, string, string) ShowToast(ToastLevel level);
    }
 
}
