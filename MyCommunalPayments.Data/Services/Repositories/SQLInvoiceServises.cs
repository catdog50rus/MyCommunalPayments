using Microsoft.EntityFrameworkCore;
using MyCommunalPayments.Data.Context;
using MyCommunalPayments.Data.Services.Repositories.Base;
using MyCommunalPayments.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyCommunalPayments.Data.Services.Repositories
{
    public class SQLInvoiceServises<T> : SQLRepository, IRepository<T> where T : InvoiceServices
    {
        public SQLInvoiceServises(DBContext context) : base(context) { }

        #region Interface

        public IEnumerable<T> GetAll()
        {
            IEnumerable<InvoiceServices> result;
            result = Context.InvoiceServices;
            return (IEnumerable<T>)result;
        }

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

        #endregion

    }
}
