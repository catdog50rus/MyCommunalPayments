using MyCommunalPayments.Data.DBModels.Models.Base;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MyCommunalPayments.Data.Interfaces.Repositories.Base
{
    public interface IBaseRepository<TEntity> where TEntity : BaseDbModel, new()
    {
        Task<ICollection<TEntity>> GetEntitiesAsync(CancellationToken cancel = default);
        Task<TEntity> GetEntityAsync(int id, CancellationToken cancel = default);
        Task<TEntity> CreateEntityAsync(TEntity entity, bool isSaveChanged = true, CancellationToken cancel = default);
        Task UpdateEntityAsync(TEntity entity, bool isSaveChanged = true, CancellationToken cancel = default);
        Task DeleteEntityAsync(int id, bool isSaveChanged = true, CancellationToken cancel = default);

        Task SaveChangedAsync(CancellationToken cancel = default);
    }
}
