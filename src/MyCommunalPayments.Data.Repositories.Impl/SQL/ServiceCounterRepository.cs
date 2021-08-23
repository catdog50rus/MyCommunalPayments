using Microsoft.EntityFrameworkCore;
using MyCommunalPayments.Data.Context;
using MyCommunalPayments.Data.DBModels.Models;
using MyCommunalPayments.Data.Interfaces.Repositories;
using MyCommunalPayments.Data.Repositories.Impl.SQL.Base;
using System.Linq;

namespace MyCommunalPayments.Data.Repositories.Impl.SQL
{
    public class ServiceCounterRepository : BaseSqlRepository<ServiceCounterDb>, IServiceCounterRepository
    {
        public ServiceCounterRepository(DBContext context) : base(context)
        {
        }

        protected override IQueryable<ServiceCounterDb> Items => base.Items
            .Include(i => i.Service);
    }
}
