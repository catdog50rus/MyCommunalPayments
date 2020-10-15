using Microsoft.EntityFrameworkCore;
using MyCommunalPayments.Data.Context;
using MyCommunalPayments.Data.Services.Repositories.Base;
using MyCommunalPayments.Models.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyCommunalPayments.Data.Services.Repositories
{
    public class SQLInvoiceServises<T> : SQLRepository, IRepository<T> where T : InvoiceServices
    {
        public SQLInvoiceServises(DBContext context) : base(context) { }

        #region Interface

        public IEnumerable<T> GetAll() => (IEnumerable<T>)Context.InvoiceServices;
        
        public void Add(T item)
        {
            if (item != null)
            {
                Context.InvoiceServices.Add(item);
                SaveChanges();
            }
        }

        public void Edit(T item)
        {
            //Вносим изменения в дело
            var temp = Context.InvoiceServices.Attach(item);
            //Применяем изменения
            temp.State = EntityState.Modified;

            SaveChanges();
        }

        public void Remove(T item)
        {
            Context.InvoiceServices.Remove(item);
            SaveChanges();
        }

        public T GetById(int id) => (T)Context.InvoiceServices.FirstOrDefault(i => i.IdInvoiceServices == id);

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

        public Task<IEnumerable<T>> Search(string serviceName, bool isCounter)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<T>> Search(string name)
        {
            throw new System.NotImplementedException();
        }

        #endregion

    }
}
