using Microsoft.AspNetCore.Components;
using MyCommunalPayments.Data.Services.Repositories.Base;
using MyCommunalPayments.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyCommunalPayments.BlazorWebUI.Pages.Services.Base
{
    public class InvoicesServicesBase : ComponentBase
    {
        #region Поля, Инициализация формы, Модальное окно

        [Parameter]
        public Invoice Invoice { get; set; }
        [Parameter]
        public IRepository<InvoiceServices> Repository { get; set; }
        [Parameter]
        public IRepository<ServiceCounter> CountersRepository { get; set; }
        [Parameter]
        public EventCallback OnClickReturnToInvoces { get; set; }
        [Parameter]
        public List<ProvidersServices> ProviderServices { get; set; }

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

        protected void CloseModal()
        {
            isCounter = default;
            modal.Close();
        }

        protected void OpenModal()
        {
            modal.ModalSize = "modal-lg";
            modal.Open();
        }

        protected override void OnInitialized()
        {
            if (Invoice != null)
            {
                provider = Invoice.Provider;
                invoiceServicesList = Repository.GetAll().Where(p => p.Invoice == Invoice);
                services = ProviderServices.Where(p => p.Provider == provider).Select(s => s.Service).ToList();
                serviceName = services[0].NameService;
                counters = CountersRepository.GetAll().ToList();
                isCounter = default;
            }

        }

        protected Service GetServiceByName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException($"{nameof(name)} не может быть пустым или иметь значение null", nameof(name));
            }

            return services.FirstOrDefault(s => s.NameService == serviceName);
        }

        #endregion

        #region Обработка нажатия кнопок

        /// <summary>
        /// Добавить дело
        /// </summary>
        protected void Add()
        {
            if (!string.IsNullOrWhiteSpace(serviceName))
            {
                service = GetServiceByName(serviceName);
                if (service.IsCounter)
                {
                    amount = counters.Where(s => s.Service == service).Select(c => c.ValueCounter).Max();
                }

                if (invoiceService == null)
                {
                    invoiceService = new InvoiceServices()
                    {
                        Invoice = Invoice,
                        Service = service,
                        Amount = amount
                    };

                    Repository.Add(invoiceService);

                }
                else
                {
                    invoiceService.Service = service;
                    Repository.Edit(invoiceService);

                }
                invoiceService = default;
                service = default;
                amount = default;
            }

            //CloseModal();
        }

        /// <summary>
        /// Изменить запись
        /// </summary>
        protected void Edit(InvoiceServices item)
        {
            invoiceService = item;
            OpenModal();
            idInvoice = item.IdInvoice;
            service = item.Service;
            serviceName = item.Service.NameService;
            amount = item.Amount;

        }

        /// <summary>
        /// Удалить запись
        /// </summary>
        protected void Remove(InvoiceServices item)
        {
            Repository.Remove(item);
        }


        protected void AddCounter(Service item)
        {
            service = item;
            invoiceService = invoiceServicesList.FirstOrDefault(s => s.Service == item);
            isCounter = true;
            amount = invoiceService.Amount;

            dateCount = DateTime.Today.ToString("dd/MM/yyyy");

            modal.ModalSize = "modal-lg";
            OpenModal();
        }

        protected void SaveCount()
        {
            if (counter == null)
            {
                counter = new ServiceCounter()
                {
                    DateCount = dateCount,
                    ValueCounter = amount,
                    Service = service
                };
                invoiceService.Amount = amount;
                Repository.Edit(invoiceService);
                CountersRepository.Add(counter);

            }
            CloseModal();
        }


        #endregion
    }
}
