using Microsoft.EntityFrameworkCore;
using MyCommunalPayments.Data.Context;
using MyCommunalPayments.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyCommunalPayments.Data.Services.Repositories
{
    public abstract class SQLRepository
    {

        public readonly DBContext Context;

        public SQLRepository(DBContext context)
        {
            Context = context;
        }

        /// <summary>
        /// Запись в БД
        /// </summary>
        protected void SaveChanges()
        {
            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Console.WriteLine("Ошибка БД!");
            }
        }

    }
}
