using System;

namespace MyCommunalPayments.Data.Services.Toast
{
    public interface IToast
    {
        (string, string, string) ShowToast(ToastLevel level);
    }

    
}
