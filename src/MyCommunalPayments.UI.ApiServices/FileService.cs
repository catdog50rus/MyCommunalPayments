using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MyCommunalPayments.Api.Infrastucture.ApiContracts;
using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using MyCommunalPayments.UI.ApiServices.Base;
using MyCommunalPayments.UI.ApiServices.Interfaces;

namespace MyCommunalPayments.UI.ApiServices
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
            if (file is null)
            {
                throw new ArgumentNullException(nameof(file));
            }

            await using var stream = file.OpenReadStream();

            var ms = new MemoryStream();
            //Присваиваем имя файла для хранения в БД
            Guid guid = Guid.NewGuid();
            var filename = $"{guid}.pdf";
            //Считываем файл в память
            await stream.CopyToAsync(ms);
            //Создаем и инициализируем экземпляр модели
            var order = new OrderContract()
            {
                OrderScreen = ms.ToArray(),
                FileName = filename
            };

            var orderId = await httpClient.PostJsonAsync<int>("api/Order", order);
            return orderId;
        }
    }
}
