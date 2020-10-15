using Microsoft.EntityFrameworkCore;
using MyCommunalPayments.Data.Context;
using MyCommunalPayments.Data.Services.Repositories.Base;
using MyCommunalPayments.Models.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyCommunalPayments.Data.Services.Repositories
{
    public class SQLInvoice<T> : SQLRepository, IRepository<T> where T : Invoice
    {
        public SQLInvoice(DBContext context) : base(context) { }

        #region Interface

        public IEnumerable<T> GetAll() => (IEnumerable<T>)Context.Invoices;

        public void Add(T item)
        {
            if (item != null)
            {
                Context.Invoices.Add(item);
                SaveChanges();
            }
        }

        public void Edit(T item)
        {
            //Вносим изменения в дело
            var temp = Context.Invoices.Attach(item);
            //Применяем изменения
            temp.State = EntityState.Modified;

            SaveChanges();
        }

        public void Remove(T item)
        {
            Context.Invoices.Remove(item);
            SaveChanges();
        }

        public T GetById(int id)
        {
            var invoice = Context.Invoices
                .Include(p=>p.Provider)
                .Include(p=>p.Period)
                .FirstOrDefault(i => i.IdInvoice == id);
            return (T)invoice;
        }

        public Task<IEnumerable<T>> GetAllAsync()
        {
            throw new System.NotImplementedException();
        }

        public Task AddAsync(T item)
        {
            throw new System.NotImplementedException();
        }

        public Task EditAsync(T item)
        {
            throw new System.NotImplementedException();
        }

        public Task RemoveAsync(T item)
        {
            throw new System.NotImplementedException();
        }

        public Task<T> GetByIdAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        Task<T> IRepository<T>.AddAsync(T item)
        {
            throw new System.NotImplementedException();
        }

        Task<T> IRepository<T>.EditAsync(T item)
        {
            throw new System.NotImplementedException();
        }

        public Task<T> RemoveAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<T>> Search(string serviceName)
        {
            throw new System.NotImplementedException();
        }

        #endregion
    }
}
