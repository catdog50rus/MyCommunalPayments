using Microsoft.EntityFrameworkCore;
using MyCommunalPayments.Data.Context;
using MyCommunalPayments.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyCommunalPayments.Data.Services.Repositories
{
    public abstract class SQLRepository
    {

        public DBContext Context { get; set; }

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
            finally
            {
                Context.Dispose();
            }
        }

    }
}
