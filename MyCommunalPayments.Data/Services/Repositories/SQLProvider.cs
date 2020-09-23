using Microsoft.EntityFrameworkCore;
using MyCommunalPayments.Data.Context;
using MyCommunalPayments.Data.Services.Repositories.Base;
using MyCommunalPayments.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyCommunalPayments.Data.Services.Repositories
{
    public class SQLProvider<T> : SQLRepository, IRepository<T> where T : Provider
    {
        public SQLProvider(DBContext context) : base(context) { }

        #region Interface

        public IEnumerable<T> GetAll() => (IEnumerable<T>)Context.Providers;

        public void Add(T item)
        {
            if (item != null)
            {
                Context.Providers.Add(item);
                SaveChanges();
            }
        }

        public void Edit(T item)
        {
            //Вносим изменения в дело
            var temp = Context.Providers.Attach(item);
            //Применяем изменения
            temp.State = EntityState.Modified;

            SaveChanges();
        }

        public void Remove(T item)
        {
            Context.Providers.Remove(item);
            SaveChanges();
        }

        public T GetById(int id) => (T)Context.Providers.FirstOrDefault(i => i.IdProvider == id);

        #endregion

    }
}
