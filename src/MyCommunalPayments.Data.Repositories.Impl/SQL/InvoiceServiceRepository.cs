using Microsoft.EntityFrameworkCore;
using MyCommunalPayments.Data.Context;
using MyCommunalPayments.Data.DBModels.Models;
using MyCommunalPayments.Data.Interfaces.Repositories;
using MyCommunalPayments.Data.Repositories.Impl.SQL.Base;
using System.Linq;

namespace MyCommunalPayments.Data.Repositories.Impl.SQL
{
    public class InvoiceServiceRepository : BaseSqlRepository<InvoiceServiceDb>, IInvoiceServiceRepository
    {
        public InvoiceServiceRepository(DBContext context) : base(context)
        {
        }

        protected override IQueryable<InvoiceServiceDb> Items => base.Items
            .Include(i => i.Invoice)
            .Include(i => i.Service);
    }
}
