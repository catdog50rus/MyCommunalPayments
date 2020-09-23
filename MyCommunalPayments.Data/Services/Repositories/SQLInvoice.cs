using Microsoft.EntityFrameworkCore;
using MyCommunalPayments.Data.Context;
using MyCommunalPayments.Data.Services.Repositories.Base;
using MyCommunalPayments.Models.Models;
using System.Collections.Generic;
using System.Linq;

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

        public T GetById(int id)=> (T)Context.Invoices.FirstOrDefault(i => i.IdInvoice == id);

        #endregion
    }
}
