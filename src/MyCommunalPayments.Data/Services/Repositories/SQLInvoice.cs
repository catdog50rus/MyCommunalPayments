using Microsoft.EntityFrameworkCore;
using MyCommunalPayments.Data.Context;
using MyCommunalPayments.Data.Services.Repositories.Base;
using MyCommunalPayments.Models.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyCommunalPayments.Data.Services.Repositories
{
    //public class SQLInvoice<T> : SQLRepository, IRepository<T> where T : Invoice
    //{
    //    public SQLInvoice(DBContext context) : base(context) { }

    //    #region AsyncInterface

    //    public async Task<IEnumerable<T>> GetAllAsync()
    //    {
    //        var result = await Context.Invoices
    //            .Include(p => p.Period)
    //            .Include(p => p.Provider)
    //            .ToListAsync();
    //        return (IEnumerable<T>)result;
    //    }

    //    public async Task<T> AddAsync(T item)
    //    {
    //        if (item == null)
    //            return null;

    //        await Context.Invoices.AddAsync(item);
    //        await Context.SaveChangesAsync();
    //        return await GetByIdAsync(item.IdInvoice);

    //    }

    //    public async Task<T> EditAsync(T item)
    //    {
    //        if (item == null)
    //            return null;

    //        var updateContent = await GetByIdAsync(item.IdInvoice);
    //        if (updateContent == null)
    //            return null;

    //        updateContent.IdPeriod = item.IdPeriod;
    //        updateContent.IdProvider = item.IdProvider;
    //        updateContent.InvoiceSum = item.InvoiceSum;
    //        updateContent.Pay = item.Pay;

    //        await Context.SaveChangesAsync();

    //        return await GetByIdAsync(updateContent.IdInvoice);

    //    }

    //    public async Task<T> GetByIdAsync(int id)
    //    {
    //        var result = await Context.Invoices
    //            .Include(p => p.Period)
    //            .Include(p => p.Provider)
    //            .FirstOrDefaultAsync(s => s.IdInvoice == id);

    //        return (T)result;
    //    }

    //    public async Task<T> RemoveAsync(int id)
    //    {
    //        var deleteContent = await GetByIdAsync(id);
    //        if (deleteContent == null)
    //            return null;

    //        Context.Invoices.Remove(deleteContent);
    //        await Context.SaveChangesAsync();
    //        return deleteContent;
    //    }

    //    public async Task<IEnumerable<T>> Search(string name)
    //    {
    //        IQueryable<T> query = (IQueryable<T>)Context.Invoices
    //            .Include(p => p.Provider)
    //            .Include(p => p.Period);

    //        if (!string.IsNullOrWhiteSpace(name))
    //        {
    //            query = query
    //                .Where(
    //                    n => n.Provider.NameProvider.Contains(name)
    //                );

    //        }
    //        return await query.ToListAsync();
    //    }

    //    #endregion
    //}
}
