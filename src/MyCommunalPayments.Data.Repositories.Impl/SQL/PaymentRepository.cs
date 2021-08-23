using Microsoft.EntityFrameworkCore;
using MyCommunalPayments.Data.Context;
using MyCommunalPayments.Data.DBModels.Models;
using MyCommunalPayments.Data.Interfaces.Repositories;
using MyCommunalPayments.Data.Repositories.Impl.SQL.Base;
using System.Linq;

namespace MyCommunalPayments.Data.Repositories.Impl.SQL
{
    public class PaymentRepository : BaseSqlRepository<PaymentDb>, IPaymentRepository
    {
        public PaymentRepository(DBContext context) : base(context)
        {
        }

        protected override IQueryable<PaymentDb> Items => base.Items
            .Include(i => i.Order)
            .Include(i => i.Invoice);
    }
}
