using Microsoft.EntityFrameworkCore;
using MyCommunalPayments.Data.Context;
using MyCommunalPayments.Data.Services.Repositories.Base;
using MyCommunalPayments.Models.Models;
using System.Collections.Generic;
using System.Linq;

namespace MyCommunalPayments.Data.Services.Repositories
{
    public class SQLServicesCounter<T> : SQLRepository, IRepository<T> where T : ServiceCounter
    {
        public SQLServicesCounter(DBContext context) : base(context) { }

        #region Interface

        public IEnumerable<T> GetAll() => (IEnumerable<T>)Context.ServiceCounters;

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

    }
}
