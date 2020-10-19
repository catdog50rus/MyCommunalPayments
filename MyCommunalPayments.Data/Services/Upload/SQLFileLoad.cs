using BlazorInputFile;
using Microsoft.EntityFrameworkCore;
using MyCommunalPayments.Data.Context;
using MyCommunalPayments.Data.Services.Repositories;
using MyCommunalPayments.Models.Models;
using System;
using System.Collections.Generic;
using System.IO;
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
            await Context.SaveChangesAsync();
            OrderId = order.IdOrder;
            return OrderId;
        }

        public async Task<Order> GetOrderById(int id)
        {
            return await Context.Orders.FirstOrDefaultAsync(i => i.IdOrder == id);
        }

        public async Task<IEnumerable<Order>> GetAll()
        {
           return await Context.Orders.ToListAsync();
        }
 

        public async Task<byte[]> GetOrder(int id)
        {
            var order = await Context.Orders.FirstOrDefaultAsync(i => i.IdOrder == id);
            var content = order.OrderScreen;

            return content;
        }
    }
}
