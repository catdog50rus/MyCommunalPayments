﻿using BlazorInputFile;
using MyCommunalPayments.Models.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyCommunalPayments.Data.Services.Upload
{
    public interface IFileLoad
    {
        int OrderId { get; }
        Task<int> UploadAsync(IFileListEntry file);
        byte[] GetOrder(int id);
        Order GetOrderById(int id);
        IEnumerable<Order> GetAll();
    }
}
