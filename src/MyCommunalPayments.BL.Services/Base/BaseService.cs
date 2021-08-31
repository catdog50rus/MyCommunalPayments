using AutoMapper;
using MyCommunalPayments.BL.Interfaces.Base;
using MyCommunalPayments.Data.DBModels.Models.Base;
using MyCommunalPayments.Data.Interfaces.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MyCommunalPayments.BL.Services.Base
{
    public abstract class BaseService<T, TEntity> : IBaseService<T> where T : class
                                                                    where TEntity: BaseDbModel, new()
    {
        private readonly IMapper  _mapper;
        private readonly IBaseRepository<TEntity>  _baseRepository;

        public BaseService(IMapper mapper,
                           IBaseRepository<TEntity> baseRepository)
        {
             _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
             _baseRepository = baseRepository ?? throw new ArgumentNullException(nameof(baseRepository));
        }

        public virtual async Task<T> CreateEntityAsync(T entity, CancellationToken cancel = default)
        {
            if (entity is null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            var dbEntity = _mapper.Map<TEntity>(entity);

            var result = await _baseRepository.CreateEntityAsync(dbEntity);

            return _mapper.Map<T>(result);
        }

        public async Task DeleteEntityAsync(int id, CancellationToken cancel = default)
        {
            if (id <= 0)
            {
                throw new ArgumentNullException(nameof(id));
            }

            await _baseRepository.DeleteEntityAsync(id, true, cancel);
        }

        public virtual async Task<ICollection<T>> GetEntitiesAsync(CancellationToken cancel = default)
        {
            var result = await _baseRepository.GetEntitiesAsync(cancel);
            return _mapper.Map<ICollection<T>>(result);
        }

        public virtual async Task<T> GetEntityAsync(int id, CancellationToken cancel = default)
        {
            if (id <= 0)
            {
                throw new ArgumentNullException(nameof(id));
            }

            var result = await _baseRepository.GetEntityAsync(id, cancel);
            return _mapper.Map<T>(result);
        }

        public virtual async Task UpdateEntityAsync(T entity, CancellationToken cancel = default)
        {
            if (entity is null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            var dbEntity = _mapper.Map<TEntity>(entity);

            await _baseRepository.UpdateEntityAsync(dbEntity);
        }
    }
}
