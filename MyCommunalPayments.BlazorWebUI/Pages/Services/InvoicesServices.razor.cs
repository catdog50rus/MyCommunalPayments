using Microsoft.AspNetCore.Components;
using MyCommunalPayments.BlazorWebUI.Components;
using MyCommunalPayments.Data.Services.ApiServices;
using MyCommunalPayments.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyCommunalPayments.BlazorWebUI.Pages.Services.Base
{
    public class InvoicesServicesBase : ComponentBase
    {
        #region Поля, Инициализация формы, Модальное окно

        [Parameter]
        public Invoice Invoice { get; set; }
        [Inject]
        public IApiRepository<InvoiceServices> Repository { get; set; }
        [Inject]
        public IApiRepository<ServiceCounter> CountersRepository { get; set; }
        [Parameter]
        public EventCallback OnClickReturnToInvoces { get; set; }
        [Inject]
        public IApiRepository<ProvidersServices> ProviderServicesRepository { get; set; }

        protected int idInvoice;

        protected InvoiceServices invoiceService;
        protected IEnumerable<InvoiceServices> invoiceServicesList;
        protected int amount;

        protected Provider provider;

        protected Service service;
        protected List<Service> services;
        protected string serviceName;

        protected ServiceCounter counter;
        protected List<ServiceCounter> counters;
        protected bool isCounter;
        protected string dateCount;

        //Модальное окно
        protected Modal modal;// { get; set; }

        protected async Task CloseModal()
        {
            isCounter = default;
            await StateUpdate();
            modal.Close();
        }

        protected void OpenModal()
        {
            modal.ModalSize = "modal-lg";
            modal.Open();
        }

        protected override async Task OnInitializedAsync()
        {
            
            if (Invoice != null) 
            {
                await StateUpdate();
                provider = Invoice.Provider;
                var pservices = await ProviderServicesRepository.GetAllAsync();
                services = pservices.Where(p => p.Provider.IdProvider == provider.IdProvider).Select(s => s.Service).ToList();
                serviceName = services[0].NameService;
            }
            counters = (await CountersRepository.GetAllAsync()).ToList();

            isCounter = default;
        }

        

        #endregion

        #region Обработка нажатия кнопок

        /// <summary>
        /// Добавить дело
        /// </summary>
        protected async Task Add()
        {
            if (!string.IsNullOrWhiteSpace(serviceName))
            {
                service = GetServiceByName(serviceName);
                if (service.IsCounter)
                {
                    amount = counters.Where(s => s.IdService == service.IdService).Select(c => c.ValueCounter).Max();
                }

                if (invoiceService == null)
                {
                    invoiceService = new InvoiceServices()
                    {
                        IdInvoice = Invoice.IdInvoice,
                        IdService = service.IdService,
                        Amount = amount
                    };

                    await Repository.AddAsync(invoiceService);

                }
                else
                {
                    invoiceService.IdService = service.IdService;
                    await Repository.EditAsync(invoiceService);

                }
                invoiceService = default;
                service = default;
                amount = default;

            }
        }

        /// <summary>
        /// Изменить запись
        /// </summary>
        protected void Edit(InvoiceServices item)
        {
            invoiceService = item;
            OpenModal();
            serviceName = item.Service.NameService;
        }

        /// <summary>
        /// Удалить запись
        /// </summary>
        protected async Task Remove(InvoiceServices item)
        {
            await Repository.RemoveAsync(item.IdInvoiceServices);
            await StateUpdate();
        }


        protected void AddCounter(InvoiceServices item)
        {
            service = item.Service;
            invoiceService = invoiceServicesList.FirstOrDefault(s => s.Service == item.Service);
            isCounter = true;
            
            amount = invoiceService.Amount;

            dateCount = DateTime.Today.ToString("dd/MM/yyyy");

            modal.ModalSize = "modal-lg";
            OpenModal();
        }

        protected async Task SaveCount()
        {
            if (counter == null)
            {
                counter = new ServiceCounter()
                {
                    DateCount = dateCount,
                    ValueCounter = amount,
                    IdService = service.IdService
                };
                invoiceService.Amount = amount;
                await Repository.EditAsync(invoiceService);
                await CountersRepository.AddAsync(counter);

            }
            await CloseModal();
        }



        #endregion

        private async Task StateUpdate()
        {
            invoiceServicesList = await Repository.GetAllAsync();
            invoiceServicesList = invoiceServicesList
                .Where(p => p.Invoice.IdInvoice == Invoice.IdInvoice);
        }

        private Service GetServiceByName(string name) => services.FirstOrDefault(s => s.NameService == name);
    }
}
