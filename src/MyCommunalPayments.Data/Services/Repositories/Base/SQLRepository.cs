using MyCommunalPayments.Data.Context;

namespace MyCommunalPayments.Data.Services.Repositories
{
    public abstract class SQLRepository
    {

        protected readonly DBContext Context;

        public SQLRepository(DBContext context)
        {
            Context = context;
        }

    }
}
