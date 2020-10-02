using BlazorInputFile;
using Microsoft.JSInterop;
using MyCommunalPayments.Data.Context;
using MyCommunalPayments.Data.Services.Repositories;
using MyCommunalPayments.Models.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCommunalPayments.Data.Services.Upload
{
    public class SQLFileLoad : SQLRepository, IFileLoad
    {
        public int OrderId { get; private set; }

        public SQLFileLoad(DBContext context) : base(context) { }

        public async Task<int> UploadAsync(IFileListEntry file)
        {
            var ms = new MemoryStream();
            Guid guid = Guid.NewGuid();
            var filename = $"{guid}.pdf";
            await file.Data.CopyToAsync(ms);

            var order = new Order()
            {
                OrderScreen = ms.ToArray(),
                FileName = filename
            };
            Context.Orders.Add(order);
            SaveChanges();
            OrderId = order.IdOrder;
            return OrderId;
        }
        public Order GetOrderById(int id) => Context.Orders.FirstOrDefault(i => i.IdOrder == id);

 

        public byte[] GetOrder(int id)
        {
            byte[] content = Context.Orders.FirstOrDefault(i => i.IdOrder == id).OrderScreen;

            return content;
        }
    }
}
