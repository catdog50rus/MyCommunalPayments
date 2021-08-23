using MyCommunalPayments.Data.Context;
using MyCommunalPayments.Data.DBModels.Models;
using MyCommunalPayments.Data.Interfaces.Repositories;
using MyCommunalPayments.Data.Repositories.Impl.SQL.Base;

namespace MyCommunalPayments.Data.Repositories.Impl.SQL
{
    public class PeriodRepository : BaseSqlRepository<PeriodDb>, IPeriodRepository
    {
        public PeriodRepository(DBContext context) : base(context)
        {
        }
    }
}
