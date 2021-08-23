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
    internal class ServiceService : IServiceService
    {
        private readonly IMapper  _mapper;
        private readonly IServiceRepository  _repository;

        public ServiceService(IMapper mapper, IServiceRepository repository)
        {
             _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
             _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<Service> CreateEntityAsync(Service entity, CancellationToken cancel = default)
        {
            if (entity is null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            var dbEntity = _mapper.Map<ServiceDb>(entity);

            var result = await _repository.CreateEntityAsync(dbEntity);

            return _mapper.Map<Service>(result);
        }

        public async Task DeleteEntityAsync(int id, CancellationToken cancel = default)
        {
            if (id <= 0)
            {
                throw new ArgumentNullException(nameof(id));
            }

            await _repository.DeleteEntityAsync(id, true, cancel);
        }

        public async Task<ICollection<Service>> GetEntitiesAsync(CancellationToken cancel = default)
        {
            var result = await _repository.GetEntitiesAsync(cancel);
            return _mapper.Map<ICollection<Service>>(result);
        }

        public async Task<Service> GetEntityAsync(int id, CancellationToken cancel = default)
        {
            if (id <= 0)
            {
                throw new ArgumentNullException(nameof(id));
            }

            var result = await _repository.GetEntityAsync(id, cancel);
            return _mapper.Map<Service>(result);
        }

        public async Task UpdateEntityAsync(Service entity, CancellationToken cancel = default)
        {
            if (entity is null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            var dbEntity = _mapper.Map<ServiceDb>(entity);

            await _repository.UpdateEntityAsync(dbEntity);
        }
    }
}
