using AutoMapper;
using MyCommunalPayments.BL.Interfaces;
using MyCommunalPayments.BL.Services.Base;
using MyCommunalPayments.Data.DBModels.Models;
using MyCommunalPayments.Data.Interfaces.Repositories;
using MyCommunalPayments.Data.Interfaces.Repositories.Base;
using MyCommunalPayments.Models.Models;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MyCommunalPayments.BL.Services
{
    internal class OrderService : IOrderService
    {
        private readonly IMapper  _mapper;
        private readonly IOrderRepository  _repository;

        public OrderService(IMapper mapper, IOrderRepository repository)
        {
             _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
             _repository = repository;
        }

        public async Task<Order> CreateEntityAsync(Order entity, CancellationToken cancel = default)
        {
            if (entity is null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            var dbEntity = _mapper.Map<OrderDb>(entity);

            var result = await _repository.CreateEntityAsync(dbEntity);

            return _mapper.Map<Order>(result);
        }

        public async Task DeleteEntityAsync(int id, CancellationToken cancel = default)
        {
            if (id <= 0)
            {
                throw new ArgumentNullException(nameof(id));
            }

            await _repository.DeleteEntityAsync(id, true, cancel);
        }

        public async Task<ICollection<Order>> GetEntitiesAsync(CancellationToken cancel = default)
        {
            var result = await _repository.GetEntitiesAsync(cancel);
            return _mapper.Map<ICollection<Order>>(result);
        }

        public async Task<Order> GetEntityAsync(int id, CancellationToken cancel = default)
        {
            if (id <= 0)
            {
                throw new ArgumentNullException(nameof(id));
            }

            var result = await _repository.GetEntityAsync(id, cancel);
            return _mapper.Map<Order>(result);
        }

        public async Task UpdateEntityAsync(Order entity, CancellationToken cancel = default)
        {
            if (entity is null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            var dbEntity = _mapper.Map<OrderDb>(entity);

            await _repository.UpdateEntityAsync(dbEntity);
        }
    }
}
