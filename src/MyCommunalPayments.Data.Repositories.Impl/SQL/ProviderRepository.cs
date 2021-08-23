using MyCommunalPayments.Data.Context;
using MyCommunalPayments.Data.DBModels.Models;
using MyCommunalPayments.Data.Interfaces.Repositories;
using MyCommunalPayments.Data.Repositories.Impl.SQL.Base;

namespace MyCommunalPayments.Data.Repositories.Impl.SQL
{
    public class ProviderRepository : BaseSqlRepository<ProviderDb>, IProviderRepository
    {
        public ProviderRepository(DBContext context) : base(context)
        {
        }
    }
}
