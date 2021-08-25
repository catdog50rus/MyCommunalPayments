using Microsoft.EntityFrameworkCore;
using MyCommunalPayments.Data.Context;
using MyCommunalPayments.Data.Services.Repositories.Base;
using MyCommunalPayments.Models.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyCommunalPayments.Data.Services.Repositories
{
    //public class SQLInvoiceServises<T> : SQLRepository, IRepository<T> where T : InvoiceServices
    //{
    //    public SQLInvoiceServises(DBContext context) : base(context) { }

    //    #region AsyncInterface

    //    public async Task<IEnumerable<T>> GetAllAsync()
    //    {
    //        var result = await Context.InvoiceServices
    //            .Include(i=>i.Invoice)
    //                .ThenInclude(p=>p.Provider)
    //            .Include(s=>s.Service)
    //            .ToListAsync();
    //        return (IEnumerable<T>)result;
    //    }

    //    public async Task<T> AddAsync(T item)
    //    {
    //        if (item != null)
    //        {
    //            await Context.InvoiceServices.AddAsync(item);
    //            await Context.SaveChangesAsync();
    //            return await GetByIdAsync(item.IdInvoiceServices);
    //        }
    //        else return null;
    //    }

    //    public async Task<T> EditAsync(T item)
    //    {
    //        if (item == null)
    //            return null;

    //        var updateContent = await GetByIdAsync(item.IdInvoiceServices);
    //        if (updateContent == null)
    //            return null;

    //        updateContent.IdService = item.IdService;
    //        updateContent.Amount = item.Amount;

    //        await Context.SaveChangesAsync();

    //        return updateContent;
    //    }

    //    public async Task<T> GetByIdAsync(int id)
    //    {
    //        var result = await Context.InvoiceServices
    //            .Include(i=>i.Invoice)
    //                .ThenInclude(p=>p.Provider)
    //            .Include(s=>s.Service)
    //            .FirstOrDefaultAsync(s => s.IdInvoiceServices == id);

    //        return (T)result;
    //    }

    //    public async Task<T> RemoveAsync(int id)
    //    {
    //        var deleteContent = await GetByIdAsync(id);
    //        if (deleteContent != null)
    //        {
    //            Context.InvoiceServices.Remove(deleteContent);
    //            await Context.SaveChangesAsync();
    //            return deleteContent;
    //        }
    //        return null;

    //    }

    //    public async Task<IEnumerable<T>> Search(string name)
    //    {
    //        IQueryable<T> query = (IQueryable<T>)Context.InvoiceServices
    //            .Include(i => i.Invoice)
    //                .ThenInclude(p => p.Provider)
    //            .Include(s => s.Service);
    //        if (!string.IsNullOrEmpty(name))
    //        {
    //            query = query
    //                .Where(
    //                    n => n.Invoice.Provider.NameProvider.Contains(name) 
    //                    || 
    //                    n.Service.NameService.Contains(name)
    //                );

    //        }
    //        return await query.ToListAsync();
    //    }

    //    #endregion

    //}
}
