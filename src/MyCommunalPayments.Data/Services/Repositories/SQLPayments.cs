using Microsoft.EntityFrameworkCore;
using MyCommunalPayments.Data.Context;
using MyCommunalPayments.Data.Services.Repositories.Base;
using MyCommunalPayments.Models.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyCommunalPayments.Data.Services.Repositories
{
    public class SQLPayments<T> : SQLRepository, IRepository<T> where T : Payment
    {
        public SQLPayments(DBContext context) : base(context) { }

        #region AsyncInterface

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            var result = await Context.Payments
                .Include(i => i.Invoice)
                    .ThenInclude(p=>p.Provider)
                .Include(i => i.Invoice)
                    .ThenInclude(p => p.Period)
                //.Include(o => o.Order)
                .ToListAsync();
            return (IEnumerable<T>)result;
        }

        public async Task<T> AddAsync(T item)
        {
            if (item != null)
            {
                await Context.Payments.AddAsync(item);
                await Context.SaveChangesAsync();
                if(item.IdOrder == 0)
                {
                    var result = await Context.Payments
                    .Include(i => i.Invoice)
                        .ThenInclude(p => p.Provider)
                    .Include(i => i.Invoice)
                        .ThenInclude(p => p.Period)
                    .FirstOrDefaultAsync(s => s.IdPayment == item.IdPayment); 
                    return (T)result;
                }
                else
                {
                    return await GetByIdAsync(item.IdPayment);
                }
 
            }
            else return null;
        }

        public async Task<T> EditAsync(T item)
        {
            if (item != null)
            {
                var updateContent = await GetByIdAsync(item.IdPayment);
                if (updateContent != null)
                {
                    updateContent.DatePayment = item.DatePayment;
                    updateContent.IdInvoice = item.IdInvoice;
                    updateContent.IdOrder = item.IdOrder;
                    updateContent.Paid = item.Paid;
                    updateContent.PaymentSum = item.PaymentSum;
                    
                    await Context.SaveChangesAsync();

                    return await GetByIdAsync(updateContent.IdPayment);
                }
                return null;
            }
            return null;

        }

        public async Task<T> GetByIdAsync(int id)
        {
            var res = await Context.Payments
                .Include(i => i.Invoice)
                    .ThenInclude(p => p.Provider)
                .Include(i => i.Invoice)
                    .ThenInclude(p => p.Period)
                //.Include(o => o.Order)
                .FirstOrDefaultAsync(s => s.IdPayment == id);

            return (T)res;

        }

        public async Task<T> RemoveAsync(int id)
        {
            var deleteContent = await GetByIdAsync(id);
            if (deleteContent != null)
            {
                Context.Payments.Remove(deleteContent);
                await Context.SaveChangesAsync();
                return deleteContent;
            }
            return null;

        }

        public async Task<IEnumerable<T>> Search(string name)
        {
            IQueryable<T> query = (IQueryable<T>)Context.Payments
                .Include(i => i.Invoice)
                    .ThenInclude(p=>p.Provider)
                .Include(i => i.Invoice)
                    .ThenInclude(p => p.Period)
                /*.Include(o => o.Order)*/;
            if (!string.IsNullOrEmpty(name))
            {
                query = query
                    .Where(p => p.Invoice.Provider.NameProvider.Contains(name));
            }
            return await query.ToListAsync();
        }

        #endregion
    }
}
