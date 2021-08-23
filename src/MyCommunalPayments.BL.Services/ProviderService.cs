using AutoMapper;
using MyCommunalPayments.BL.Interfaces;
using MyCommunalPayments.Data.DBModels.Models;
using MyCommunalPayments.Data.Interfaces.Repositories;
using MyCommunalPayments.Models.Models;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MyCommunalPayments.BL.Services
{
    internal class ProviderService : IProviderService
    {
        private readonly IMapper  _mapper;
        private readonly IProviderRepository  _repository;

        public ProviderService(IMapper mapper, IProviderRepository repository)
        {
             _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
             _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<Provider> CreateEntityAsync(Provider entity, CancellationToken cancel = default)
        {
            if (entity is null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            var dbEntity = _mapper.Map<ProviderDb>(entity);

            var result = await _repository.CreateEntityAsync(dbEntity);

            return _mapper.Map<Provider>(result);
        }

        public async Task DeleteEntityAsync(int id, CancellationToken cancel = default)
        {
            if (id <= 0)
            {
                throw new ArgumentNullException(nameof(id));
            }

            await _repository.DeleteEntityAsync(id, true, cancel);
        }

        public async Task<ICollection<Provider>> GetEntitiesAsync(CancellationToken cancel = default)
        {
            var result = await _repository.GetEntitiesAsync(cancel);
            return _mapper.Map<ICollection<Provider>>(result);
        }

        public async Task<Provider> GetEntityAsync(int id, CancellationToken cancel = default)
        {
            if (id <= 0)
            {
                throw new ArgumentNullException(nameof(id));
            }

            var result = await _repository.GetEntityAsync(id, cancel);
            return _mapper.Map<Provider>(result);
        }

        public async Task UpdateEntityAsync(Provider entity, CancellationToken cancel = default)
        {
            if (entity is null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            var dbEntity = _mapper.Map<ProviderDb>(entity);

            await _repository.UpdateEntityAsync(dbEntity);
        }
    }
}
