using Microsoft.AspNetCore.Components;
using MyCommunalPayments.BlazorWebUI.Components;
using MyCommunalPayments.Data.Services.ApiServices;
using MyCommunalPayments.Models.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyCommunalPayments.BlazorWebUI.Pages.Services.Base
{
    public class InvoicesServicesBase : ComponentBase
    {
        #region Поля, Инициализация формы, Модальное окно

        /// <summary>
        /// Модель представления
        /// </summary>
        protected InvoiceServiceViewModel InvoiceServiceModel = new InvoiceServiceViewModel();

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

        

        protected InvoiceServices invoiceService;
        protected IEnumerable<InvoiceServices> invoiceServicesList;
        protected int amount;
        //protected int idInvoice;

        protected Provider provider;

        protected Service service;
        protected List<Service> services;
        protected int IdService;

        protected ServiceCounter counter;
        protected List<ServiceCounter> counters;
        protected string dateCount;

        //Модальное окно
        protected Modal modal;

        protected void CloseModal()
        {
            modal.Close();
        }

        protected void OpenModal()
        {
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
            }
            counters = (await CountersRepository.GetAllAsync()).ToList();
        }



        #endregion

        #region Обработка нажатия кнопок

        /// <summary>
        /// Добавить дело
        /// </summary>
        protected async Task AddAsync()
        {
            IdService = int.Parse(InvoiceServiceModel.IdService);
            service = services.FirstOrDefault(s => s.IdService == IdService);

            if(service != null)
            {
                if (service.IsCounter)
                {
                    amount = counters.Where(s => s.IdService == IdService).Select(c => c.ValueCounter).Max();
                }

                if (invoiceService == null)
                {
                    invoiceService = new InvoiceServices()
                    {
                        IdInvoice = Invoice.IdInvoice,
                        IdService = IdService,
                        Amount = amount
                    };
                    if(invoiceServicesList.FirstOrDefault(ins=>ins.Equals(invoiceService)) == null)
                    {
                        await Repository.AddAsync(invoiceService);
                    }
                }
                else
                {
                    invoiceService.IdService = IdService;
                    await Repository.EditAsync(invoiceService);
                }
            }
            
            invoiceService = default;
            service = default;
            amount = default;
            await StateUpdate();
            CloseModal();

        }

        /// <summary>
        /// Изменить запись
        /// </summary>
        protected void Edit(InvoiceServices item)
        {
            invoiceService = item;
            OpenModal();
            InvoiceServiceModel.IdService = invoiceService.Invoice.ToString();
        }

        /// <summary>
        /// Удалить запись
        /// </summary>
        protected async Task Remove(InvoiceServices item)
        {
            await Repository.RemoveAsync(item.IdInvoiceServices);
            await StateUpdate();
        }


        #endregion

        private async Task StateUpdate()
        {
            invoiceServicesList = await Repository.GetAllAsync();
            invoiceServicesList = invoiceServicesList
                .Where(p => p.Invoice.IdInvoice == Invoice.IdInvoice);
        }
    }

    public class InvoiceServiceViewModel
    {
        [Required(ErrorMessage = "Необходимо выбрать услугу")]
        public string IdService { get; set; } = "";
    }
}
