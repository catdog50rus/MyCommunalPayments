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
    internal class ServiceCounterService : IServiceCounterService
    {
        private readonly IMapper  _mapper;
        private readonly IServiceCounterRepository  _repository;

        public ServiceCounterService(IMapper mapper, IServiceCounterRepository repository)
        {
             _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
             _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<ServiceCounter> CreateEntityAsync(ServiceCounter entity, CancellationToken cancel = default)
        {
            if (entity is null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            var dbEntity = _mapper.Map<ServiceCounterDb>(entity);

            var result = await _repository.CreateEntityAsync(dbEntity);

            return _mapper.Map<ServiceCounter>(result);
        }

        public async Task DeleteEntityAsync(int id, CancellationToken cancel = default)
        {
            if (id <= 0)
            {
                throw new ArgumentNullException(nameof(id));
            }

            await _repository.DeleteEntityAsync(id, true, cancel);
        }

        public async Task<ICollection<ServiceCounter>> GetEntitiesAsync(CancellationToken cancel = default)
        {
            var result = await _repository.GetEntitiesAsync(cancel);
            return _mapper.Map<ICollection<ServiceCounter>>(result);
        }

        public async Task<ServiceCounter> GetEntityAsync(int id, CancellationToken cancel = default)
        {
            if (id <= 0)
            {
                throw new ArgumentNullException(nameof(id));
            }

            var result = await _repository.GetEntityAsync(id, cancel);
            return _mapper.Map<ServiceCounter>(result);
        }

        public async Task UpdateEntityAsync(ServiceCounter entity, CancellationToken cancel = default)
        {
            if (entity is null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            var dbEntity = _mapper.Map<ServiceCounterDb>(entity);

            await _repository.UpdateEntityAsync(dbEntity);
        }
    }
}
