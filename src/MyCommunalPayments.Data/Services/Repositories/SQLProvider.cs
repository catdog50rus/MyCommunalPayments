using Microsoft.EntityFrameworkCore;
using MyCommunalPayments.Data.Context;
using MyCommunalPayments.Data.Services.Repositories.Base;
using MyCommunalPayments.Models.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyCommunalPayments.Data.Services.Repositories
{
    //public class SQLProvider<T> : SQLRepository, IRepository<T> where T : Provider
    //{
    //    public SQLProvider(DBContext context) : base(context) { }

    //    #region AsyncInterface

    //    public async Task<IEnumerable<T>> GetAllAsync()
    //    {
    //        var result = await Context.Providers.ToListAsync();
    //        return (IEnumerable<T>)result;
    //    }

    //    public async Task<T> AddAsync(T item)
    //    {
    //        if (item != null)
    //        {
    //            var result = await Context.Providers.AddAsync(item);
    //            await Context.SaveChangesAsync();
    //            return (T)result.Entity;
    //        }
    //        else return null;
    //    }

    //    public async Task<T> EditAsync(T item)
    //    {
    //        if (item != null)
    //        {
    //            var updateContent = await GetByIdAsync(item.IdProvider);
    //            if (updateContent != null)
    //            {
    //                updateContent.NameProvider = item.NameProvider;
    //                updateContent.WebSite = item.WebSite;

    //                await Context.SaveChangesAsync();

    //                return updateContent;
    //            }
    //            return null;
    //        }
    //        return null;

    //    }

    //    public async Task<T> GetByIdAsync(int id)
    //    {
    //        var res = await Context.Providers.FirstOrDefaultAsync(s => s.IdProvider == id);
    //        return (T)res;
    //    }

    //    public async Task<T> RemoveAsync(int id)
    //    {
    //        var deleteContent = await GetByIdAsync(id);
    //        if (deleteContent != null)
    //        {
    //            Context.Providers.Remove(deleteContent);
    //            await Context.SaveChangesAsync();
    //            return deleteContent;
    //        }
    //        return null;

    //    }

    //    public async Task<IEnumerable<T>> Search(string name)
    //    {
    //        IQueryable<T> query = (IQueryable<T>)Context.Providers;
    //        if (!string.IsNullOrEmpty(name))
    //        {
    //            query = query
    //                .Where(n => n.NameProvider.Contains(name));

    //        }
    //        return await query.ToListAsync();
    //    }

    //    #endregion

    //}
}
