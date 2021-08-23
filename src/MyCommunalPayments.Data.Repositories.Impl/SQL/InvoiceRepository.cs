using Microsoft.EntityFrameworkCore;
using MyCommunalPayments.Data.Context;
using MyCommunalPayments.Data.DBModels.Models;
using MyCommunalPayments.Data.Interfaces.Repositories;
using MyCommunalPayments.Data.Repositories.Impl.SQL.Base;
using System.Linq;

namespace MyCommunalPayments.Data.Repositories.Impl.SQL
{
    public class InvoiceRepository : BaseSqlRepository<InvoiceDb>, IInvoiceRepository
    {
        public InvoiceRepository(DBContext context) : base(context)
        {
        }

        protected override IQueryable<InvoiceDb> Items => base.Items
            .Include(i => i.Period)
            .Include(i => i.Provider);
    }
}
