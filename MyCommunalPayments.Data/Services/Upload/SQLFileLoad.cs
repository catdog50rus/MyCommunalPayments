using Microsoft.EntityFrameworkCore;
using MyCommunalPayments.Data.Context;
using MyCommunalPayments.Data.Services.Repositories;
using MyCommunalPayments.Models.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyCommunalPayments.Data.Services.Upload
{
    public class SQLFileLoad : SQLRepository, IFileLoad
    {
        public int OrderId { get; private set; }

        public SQLFileLoad(DBContext context) : base(context) { }

        public async Task UploadAsync(Order order)
        {
            Context.Orders.Add(order);
            await Context.SaveChangesAsync();
            OrderId = order.IdOrder;
        }

        public async Task<Order> GetOrderById(int id)
        {
            return await Context.Orders.FirstOrDefaultAsync(i => i.IdOrder == id);
        }

        public async Task<byte[]> GetOrder(int id)
        {
            var order = await Context.Orders.FirstOrDefaultAsync(i => i.IdOrder == id);
            var content = order.OrderScreen;

            return content;
        }

        public async Task<bool> RemoveAsync(int id)
        {
            var order = await GetOrderById(id);
            if (order == null) return false;

            Context.Remove(order);
            await Context.SaveChangesAsync();
            return true;

        }
    }
}
