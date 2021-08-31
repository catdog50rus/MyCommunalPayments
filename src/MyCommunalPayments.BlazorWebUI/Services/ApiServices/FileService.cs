using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
using MyCommunalPayments.Api.Infrastucture.ApiContracts;
using MyCommunalPayments.BlazorWebUI.Services.ApiServices.Base;
using MyCommunalPayments.BlazorWebUI.Services.ApiServices.Interfaces;
using MyCommunalPayments.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace MyCommunalPayments.BlazorWebUI.Services.ApiServices
{
    public class FileService : BaseHttpClient, IFileService
    {
        public FileService(HttpClient httpClient) : base(httpClient)
        {
        }

        public async Task<OrderContract> GetOrderById(int id)
        {
            var order = await httpClient.GetJsonAsync<OrderContract>($"api/Order/{id}");
            return order;
        }

        public async Task<bool> RemoveAsync(int id)
        {
            await httpClient.DeleteAsync($"api/Order/{id}");
            return true;
        }

        public async Task<int> UploadFile(IBrowserFile file)
        {
            var orderId = await httpClient.SendJsonAsync<int>(HttpMethod.Post ,"api/Order", file);
            return 0;
        }
    }
}

namespace MyCommunalPayments.BlazorWebUI.Services.ApiServices.Interfaces
{
    public interface IFileService
    {
        Task<int> UploadFile(IBrowserFile file);
        Task<OrderContract> GetOrderById(int id);
        Task<bool> RemoveAsync(int id);
    }
}
