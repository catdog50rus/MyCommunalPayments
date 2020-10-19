using Microsoft.EntityFrameworkCore;
using MyCommunalPayments.Data.Context;
using MyCommunalPayments.Data.Services.Repositories.Base;
using MyCommunalPayments.Models.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyCommunalPayments.Data.Services.Repositories
{
    public class SQLProvidersServices<T> : SQLRepository, IRepository<T> where T : ProvidersServices
    {
        public SQLProvidersServices(DBContext context) : base(context) { }

        #region AsyncInterface

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            var result = await Context.ProvidersServices
                .Include(p=>p.Provider)
                .Include(s=>s.Service)
                .OrderBy(p=>p.Id)
                .ToListAsync();
            return (IEnumerable<T>)result;
        }

        public async Task<T> AddAsync(T item)
        {
            if (item != null)
            {
                var result = await Context.ProvidersServices.AddAsync(item);
                await Context.SaveChangesAsync();
                return (T)result.Entity;
            }
            else return null;
        }

        public async Task<T> EditAsync(T item)
        {
            if (item != null)
            {
                var updateContent = await GetByIdAsync(item.Id);
                if (updateContent != null)
                {
                    updateContent.IdService = item.IdService;

                    await Context.SaveChangesAsync();

                    return await GetByIdAsync(updateContent.Id);
                }
                return null;
            }
            return null;

        }

        public async Task<T> GetByIdAsync(int id)
        {
            var res = await Context.ProvidersServices
                .Include(p=>p.Provider)
                .Include(s=>s.Service)
                .FirstOrDefaultAsync(s => s.Id == id);
            return (T)res;
        }

        public async Task<T> RemoveAsync(int id)
        {
            var deleteContent = await GetByIdAsync(id);
            if (deleteContent != null)
            {
                Context.ProvidersServices.Remove(deleteContent);
                await Context.SaveChangesAsync();
                return deleteContent;
            }
            return null;

        }

        public async Task<IEnumerable<T>> Search(string name)
        {
            IQueryable<T> query = (IQueryable<T>)Context.ProvidersServices
                .Include(p => p.Provider)
                .Include(s => s.Service);
            if (!string.IsNullOrEmpty(name))
            {
                query = query
                    .Where(p => p.Provider.NameProvider.Contains(name) || p.Service.NameService.Contains(name));
            }
            return await query.ToListAsync();
        }

        #endregion
    }
}
