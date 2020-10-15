using Microsoft.EntityFrameworkCore;
using MyCommunalPayments.Data.Context;
using MyCommunalPayments.Data.Services.Repositories.Base;
using MyCommunalPayments.Models.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyCommunalPayments.Data.Services.Repositories
{
    public class SQLServicesCounter<T> : SQLRepository, IRepository<T> where T : ServiceCounter
    {
        public SQLServicesCounter(DBContext context) : base(context) { }

        #region Interface

        public IEnumerable<T> GetAll() => (IEnumerable<T>)Context.ServiceCounters
            .Include(s => s.Service);
            

        public void Add(T item)
        {
            if (item != null)
            {
                Context.ServiceCounters.Add(item);
                SaveChanges();
            }
        }

        public void Edit(T item)
        {
            //Вносим изменения в дело
            var temp = Context.ServiceCounters.Attach(item);
            //Применяем изменения
            temp.State = EntityState.Modified;

            SaveChanges();
        }

        public void Remove(T item)
        {
            Context.ServiceCounters.Remove(item);
            SaveChanges();
        }

        public T GetById(int id) => (T)Context.ServiceCounters.FirstOrDefault(i => i.IdCounter == id);

        #endregion

        #region AsyncInterface

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            var result = await Context.ServiceCounters
                .Include(s => s.Service)
                .ToListAsync();
            result = result.OrderByDescending(s => s.ToSort()).ToList();
            return (IEnumerable<T>)result;
        }

        public async Task<T> AddAsync(T item)
        {
            if (item != null)
            {
                await Context.ServiceCounters.AddAsync(item);
                await Context.SaveChangesAsync();
                return await GetByIdAsync(item.IdCounter);
            }
            else return null;
        }

        public async Task<T> EditAsync(T item)
        {
            if (item != null)
            {
                var updateContent = await GetByIdAsync(item.IdCounter);
                if (updateContent != null)
                {
                    updateContent.IdService = item.IdService;
                    updateContent.DateCount = item.DateCount;
                    updateContent.ValueCounter = item.ValueCounter;

                    await Context.SaveChangesAsync();

                    return await GetByIdAsync(updateContent.IdCounter);
                }
                return null;
            }
            return null;

        }

        public async Task<T> GetByIdAsync(int id)
        {
            var res = await Context.ServiceCounters
                .Include(s => s.Service)
                .FirstOrDefaultAsync(s => s.IdCounter == id);
            return (T)res;
        }

        public async Task<T> RemoveAsync(int id)
        {
            var deleteContent = await GetByIdAsync(id);
            if (deleteContent != null)
            {
                Context.ServiceCounters.Remove(deleteContent);
                await Context.SaveChangesAsync();
                return deleteContent;
            }
            return null;

        }

        public async Task<IEnumerable<T>> Search(string name)
        {
            IQueryable<T> query = (IQueryable<T>)Context.ServiceCounters
                .Include(s => s.Service);
            if (!string.IsNullOrEmpty(name))
            {
                query = query
                    .Where(p => p.Service.NameService.Contains(name));
            }
            return await query.ToListAsync();
        }

        #endregion


    }
}
