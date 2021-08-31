using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
using MyCommunalPayments.Api.Infrastucture.ApiContracts;
using MyCommunalPayments.BlazorWebUI.Services.ApiServices.Base;
using MyCommunalPayments.BlazorWebUI.Services.ApiServices.Interfaces;
using MyCommunalPayments.Models.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace MyCommunalPayments.BlazorWebUI.Services.ApiServices
{
    public class FileService : BaseHttpClient, IFileService
    {
        private static readonly Dictionary<string, List<byte[]>> _fileSignatires = new()
        {
            {
                ".pdf",
                new List<byte[]>
                {
                    new byte[] { 25, 50, 44, 46 }
                }
            }
        };


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

            //bool fileSignatureValidation = FileSignatureValidator(file);

            //if (!fileSignatureValidation)
            //    throw new Exception("Файл не соответствует типу");

            using var stream = file.OpenReadStream();

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


        private bool FileSignatureValidator(IBrowserFile file)
        {
            var fileExtention = Path.GetExtension(file.Name);
            var signature = _fileSignatires[fileExtention];
            if (signature is null || signature.Count == 0)
                return false;

            using var reader = new BinaryReader(file.OpenReadStream());
            var headBytes = reader.ReadBytes(signature.Max(s => s.Length));
            if (!signature.Any(signature => headBytes.Take(signature.Length).SequenceEqual(signature)))
                return false;

            return true;
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
