using Microsoft.EntityFrameworkCore;
using MyCommunalPayments.Data.Context;
using MyCommunalPayments.Data.Services.Repositories.Base;
using MyCommunalPayments.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyCommunalPayments.Data.Services.Repositories
{
    public class SQLPeriod<T> : SQLRepository, IRepository<T> where T : Period 
    {
        public SQLPeriod(DBContext context) : base(context) { }

        #region Interface

        public IEnumerable<T> GetAll() => (IEnumerable<T>)Context.Periods;

        public void Add(T item)
        {
            if (item != null)
            {
                Context.Periods.Add(item);
                SaveChanges();
            }
        }

        public void Edit(T item)
        {
            //Вносим изменения в дело
            var temp = Context.Periods.Attach(item);
            //Применяем изменения
            temp.State = EntityState.Modified;

            SaveChanges();
        }

        public void Remove(T item)
        {
            Context.Periods.Remove(item);
            SaveChanges();
        }

        public T GetById(int id) => (T)Context.Periods.FirstOrDefault(i => i.IdKey == id);

        #endregion

    }


}
