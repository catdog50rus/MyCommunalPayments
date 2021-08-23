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
    internal class InvoiceService : IInvoiceService
    {
        private readonly IMapper _mapper;
        private readonly IInvoiceRepository  _repository;

        public InvoiceService(IMapper mapper,
                              IInvoiceRepository repository)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
             _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public virtual async Task<Invoice> CreateEntityAsync(Invoice entity, CancellationToken cancel = default)
        {
            if (entity is null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            var dbEntity = _mapper.Map<InvoiceDb>(entity);

            var result = await _repository.CreateEntityAsync(dbEntity);

            return _mapper.Map<Invoice>(result);
        }

        public async Task DeleteEntityAsync(int id, CancellationToken cancel = default)
        {
            if (id <= 0)
            {
                throw new ArgumentNullException(nameof(id));
            }

            await _repository.DeleteEntityAsync(id, true, cancel);
        }

        public virtual async Task<ICollection<Invoice>> GetEntitiesAsync(CancellationToken cancel = default)
        {
            var result = await _repository.GetEntitiesAsync(cancel);
            return _mapper.Map<ICollection<Invoice>>(result);
        }

        public virtual async Task<Invoice> GetEntityAsync(int id, CancellationToken cancel = default)
        {
            if (id <= 0)
            {
                throw new ArgumentNullException(nameof(id));
            }

            var result = await _repository.GetEntityAsync(id, cancel);
            return _mapper.Map<Invoice>(result);
        }

        public virtual async Task UpdateEntityAsync(Invoice entity, CancellationToken cancel = default)
        {
            if (entity is null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            var dbEntity = _mapper.Map<InvoiceDb>(entity);

            await _repository.UpdateEntityAsync(dbEntity);
        }
    }
}
