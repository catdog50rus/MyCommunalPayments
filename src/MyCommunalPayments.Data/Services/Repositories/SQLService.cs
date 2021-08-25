using Microsoft.EntityFrameworkCore;
using MyCommunalPayments.Data.Context;
using MyCommunalPayments.Data.Services.Repositories.Base;
using MyCommunalPayments.Models.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyCommunalPayments.Data.Services.Repositories
{
    //public class SQLService<T> : SQLRepository, IRepository<T> where T:Service
    //{
    //    public SQLService(DBContext context) : base(context) { }

    //    #region InterfaseAsync

    //    public async Task<IEnumerable<T>> GetAllAsync()
    //    {
    //        var result = await Context.Services.ToListAsync();
    //        return (IEnumerable<T>)result;
    //    }

    //    public async Task<T> AddAsync(T item)
    //    {
    //        if (item != null)
    //        {
    //            var result = await Context.Services.AddAsync(item);
    //            await Context.SaveChangesAsync();
    //            return (T)result.Entity;
    //        }
    //        else return null;
    //    }

    //    public async Task<T> EditAsync(T item)
    //    {
    //        if(item != null)
    //        {
    //            var updateContent = await GetByIdAsync(item.IdService);
    //            if(updateContent != null)
    //            {
    //                updateContent.IsCounter = item.IsCounter;
    //                updateContent.NameService = item.NameService;

    //                await Context.SaveChangesAsync();

    //                return updateContent;
    //            }
    //            return null;
    //        }
    //        return null;
            
    //    }

    //    public async Task<T> GetByIdAsync(int id)
    //    {
    //        var res = await Context.Services.FirstOrDefaultAsync(s => s.IdService == id);
    //        return (T)res;
    //    }

    //    public async Task<T> RemoveAsync(int id)
    //    {
    //        var deleteContent = await GetByIdAsync(id);
    //        if (deleteContent != null) 
    //        {
    //            Context.Services.Remove(deleteContent);
    //            await Context.SaveChangesAsync();
    //            return deleteContent;
    //        }
    //        return null;
            
    //    }

    //    public async Task<IEnumerable<T>> Search(string serviceName)
    //    {
    //        IQueryable<T> query = (IQueryable<T>)Context.Services;
    //        if (!string.IsNullOrEmpty(serviceName))
    //        {
    //            query = query
    //                .Where(n => n.NameService.Contains(serviceName));
                    
    //        }
    //        return await query.ToListAsync();
    //    }


    //    #endregion


    //}
}
