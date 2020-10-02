using BlazorInputFile;
using Microsoft.JSInterop;
using MyCommunalPayments.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyCommunalPayments.Data.Services.Upload
{
    public interface IFileLoad
    {
        int OrderId { get; }
        Task<int> UploadAsync(IFileListEntry file);
        byte[] GetOrder(int id);
        Order GetOrderById(int id);
    }
}
