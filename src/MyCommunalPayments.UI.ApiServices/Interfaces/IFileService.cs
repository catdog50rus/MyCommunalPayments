using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Forms;
using MyCommunalPayments.Api.Infrastucture.ApiContracts;

namespace MyCommunalPayments.UI.ApiServices.Interfaces
{
    public interface IFileService
    {
        Task<int> UploadFile(IBrowserFile file);
        Task<OrderContract> GetOrderById(int id);
        Task<bool> RemoveAsync(int id);
    }
}