using Microsoft.EntityFrameworkCore;
using MyCommunalPayments.Data.Context;
using MyCommunalPayments.Data.Services.Repositories.Base;
using MyCommunalPayments.Models.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyCommunalPayments.Data.Services.Repositories
{
    public class SQLPeriod<T> : SQLRepository, IRepository<T> where T : Period 
    {
        public SQLPeriod(DBContext context) : base(context) { }

        #region AsyncInterface


        public async Task<IEnumerable<T>> GetAllAsync()
        {
            var result = await Context.Periods.ToListAsync();
            return (IEnumerable<T>)result;
        }

        public async Task<T> AddAsync(T item)
        {
            if (item != null)
            {
                var result = await Context.Periods.AddAsync(item);
                await Context.SaveChangesAsync();
                return (T)result.Entity;
            }
            else return null;
        }

        public async Task<T> EditAsync(T item)
        {
            if (item != null)
            {
                var updateContent = await GetByIdAsync(item.IdKey);
                if (updateContent != null)
                {
                    updateContent.Month = item.Month;
                    updateContent.Year = item.Year;

                    await Context.SaveChangesAsync();

                    return updateContent;
                }
                return null;
            }
            return null;

        }

        public async Task<T> GetByIdAsync(int id)
        {
            var res = await Context.Periods.FirstOrDefaultAsync(s => s.IdKey == id);
            return (T)res;
        }

        public async Task<T> RemoveAsync(int id)
        {
            var deleteContent = await GetByIdAsync(id);
            if (deleteContent != null)
            {
                Context.Periods.Remove(deleteContent);
                await Context.SaveChangesAsync();
                return deleteContent;
            }
            return null;

        }

        public async Task<IEnumerable<T>> Search(string name)
        {
            IQueryable<T> query = (IQueryable<T>)Context.Periods;
            if (!string.IsNullOrEmpty(name))
            {
                query = query
                    .Where(n => n.ToString().Contains(name));

            }
            return await query.ToListAsync();
        }


        #endregion

    }


}
