using Microsoft.AspNetCore.Components.Forms;
using MyCommunalPayments.Api.Infrastucture.ApiContracts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCommunalPayments.Api.Infrastucture.ApiServices
{
    public class ApiFileService : IApiFileService
    {

        private static readonly Dictionary<string, List<byte[]>> _fileSignatires = new()
        {
            {
                ".pdf",
                new List<byte[]>
                {
                    new byte[] { 0x25, 0x50, 0x44, 0x46 }
                }
            }
        };

        public async Task<OrderContract> UploadFileAsync(IBrowserFile file)
        {
            if (file is null)
            {
                throw new ArgumentNullException(nameof(file));
            }

            bool fileSignatureValidation = FileSignatureValidator(file);

            if (!fileSignatureValidation)
                throw new Exception("Файл не соответствует типу");

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

            return order;
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

    public interface IApiFileService
    {
        Task<OrderContract> UploadFileAsync(IBrowserFile file);
    }
}
