using Microsoft.EntityFrameworkCore;
using MyCommunalPayments.Data.Context;
using MyCommunalPayments.Data.Services.Repositories.Base;
using MyCommunalPayments.Models.Models;
using System.Collections.Generic;
using System.Linq;

namespace MyCommunalPayments.Data.Services.Repositories
{
    public class SQLProvidersServices<T> : SQLRepository, IRepository<T> where T : ProvidersServices
    {
        public SQLProvidersServices(DBContext context) : base(context) { }

        #region Interface

        public IEnumerable<T> GetAll() => (IEnumerable<T>)Context.ProvidersServices;

        public void Add(T item)
        {
            if (item != null)
            {
                Context.ProvidersServices.Add(item);
                SaveChanges();
            }
        }

        public void Edit(T item)
        {
            //Вносим изменения в дело
            var temp = Context.ProvidersServices.Attach(item);
            //Применяем изменения
            temp.State = EntityState.Modified;

            SaveChanges();
        }

        public void Remove(T item)
        {
            Context.ProvidersServices.Remove(item);
            SaveChanges();
        }

        public T GetById(int id) => (T)Context.ProvidersServices.FirstOrDefault(i => i.Id == id);

        #endregion
    }
}
