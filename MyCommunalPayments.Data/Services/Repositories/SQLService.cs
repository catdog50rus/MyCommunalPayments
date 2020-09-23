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
    public class SQLService<T> : SQLRepository, IRepository<T> where T:Service
    {
        public SQLService(DBContext context) : base(context) { }

        #region Interface

        public IEnumerable<T> GetAll() => (IEnumerable<T>)Context.Services;

        public void Add(T item)
        {
            if(item != null)
            {
                Context.Services.Add(item);
                SaveChanges();
            }
        }

        public void Edit(T item)
        {
            //Вносим изменения в дело
            var temp = Context.Services.Attach(item);
            //Применяем изменения
            temp.State = EntityState.Modified;

            SaveChanges();
        }

        public void Remove(T item)
        {
            Context.Services.Remove(item);
            SaveChanges();
        }

        public T GetById(int id) => (T)Context.Services.FirstOrDefault(i => i.IdService == id);

        #endregion


    }
}
