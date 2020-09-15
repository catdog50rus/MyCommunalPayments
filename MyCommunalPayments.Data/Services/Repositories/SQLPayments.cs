using Microsoft.EntityFrameworkCore;
using MyCommunalPayments.Data.Context;
using MyCommunalPayments.Data.Services.Repositories.Base;
using MyCommunalPayments.Models.Models;
using System.Collections.Generic;

namespace MyCommunalPayments.Data.Services.Repositories
{
    public class SQLPayments<T> : SQLRepository, IRepository<T> where T : Payment
    {
        public SQLPayments(DBContext context) : base(context) { }

        #region Interface

        public IEnumerable<T> GetAll()
        {
            IEnumerable<Payment> result;
            result = Context.Payments;
            return (IEnumerable<T>)result;
        }

        public void Add(T item)
        {
            if (item != null)
            {
                Context.Payments.Add(item);
                SaveChanges();
            }
        }

        public void Edit(T item)
        {
            //Вносим изменения в дело
            var temp = Context.Payments.Attach(item);
            //Применяем изменения
            temp.State = EntityState.Modified;

            SaveChanges();
        }

        public void Remove(T item)
        {
            Context.Payments.Remove(item);
            SaveChanges();
        }

        #endregion
    }
}
