using Microsoft.AspNetCore.Components;
using MyCommunalPayments.Data.Services.Repositories.Base;
using MyCommunalPayments.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyCommunalPayments.BlazorWebUI.Pages.Services.Base
{
    public class ServicesCountersBase : ComponentBase
    {
        #region Поля, Инициализация формы, Модальное окно

        [Parameter]
        public string IdInvoice { get; set; }
        [Inject]
        public IRepository<ServiceCounter> Repository { get; set; }
        [Inject]
        public IRepository<Service> RepositoryServices { get; set; }
        [Inject]
        public IRepository<Invoice> RepositoryInvoices { get; set; }
        [Inject]
        public IRepository<ProvidersServices> RepositoryProviders { get; set; }
        [Inject]
        public IRepository<Provider> RepositoryProvider { get; set; }

        protected Invoice invoice;
        protected List<Invoice> invoices;

        protected IEnumerable<ServiceCounter> serviceCounters;
        protected ServiceCounter serviceCounter;
        protected string dateCount;
        protected int valueCounter;

        protected List<Service> services;
        protected Service service;

        protected List<ProvidersServices> providers;
        protected Provider provider;
        protected string serviceName;
        protected List<Provider> providerss;

        //Модальное окно
        protected Modal modal;// { get; set; }

        protected void CloseModal()
        {
            serviceCounter = default;
            dateCount = default;
            valueCounter = default;
            modal.Close();
        }
        protected void OpenModal()
        {
            dateCount = DateTime.Today.ToString("dd/MM/yyyy");
            modal.ModalSize = "modal-lg";
            modal.Open();
        }

        protected override void OnInitialized()
        {

            if (!string.IsNullOrEmpty(IdInvoice))
            {
                int.TryParse(IdInvoice, out int invoiceId);
                if (invoiceId > 0)
                {
                    invoice = RepositoryInvoices.GetById(invoiceId);
                    providerss = RepositoryProvider.GetAll().ToList();
                    provider = RepositoryProviders.GetAll().FirstOrDefault(p => p.Provider == invoice.Provider).Provider;
                    services = RepositoryProviders.GetAll().Where(p => p.IdProvider == provider.IdProvider).Select(s => s.Service).ToList();
                }
            }
            else
            {

            }
            serviceCounters = Repository.GetAll().OrderByDescending(d => d.ToSort()).ThenBy(s => s.Service.NameService);

            providers = RepositoryProviders.GetAll().ToList();
            invoices = RepositoryInvoices.GetAll().ToList();
            services = RepositoryServices.GetAll().Where(i => i.IsCounter).ToList();
            serviceName = services[0].NameService;



        }

        protected Service GetServiceByName(string name)
        {
            return services.FirstOrDefault(s => s.NameService == name);
        }


        #endregion

        #region Обработка нажатия кнопок

        /// <summary>
        /// Добавить или отредактировать
        /// </summary>
        protected void Add()
        {
            if (!string.IsNullOrWhiteSpace(dateCount) && !string.IsNullOrWhiteSpace(serviceName) && valueCounter >= 0)
            {

                if (serviceCounter == null)
                {
                    serviceCounter = new ServiceCounter()
                    {
                        DateCount = dateCount,
                        ValueCounter = valueCounter,
                        Service = GetServiceByName(serviceName)
                    };

                    Repository.Add(serviceCounter);
                }
                else
                {
                    serviceCounter.DateCount = dateCount;
                    serviceCounter.Service = GetServiceByName(serviceName);
                    serviceCounter.ValueCounter = valueCounter;
                    Repository.Edit(serviceCounter);
                }
            }

            CloseModal();
        }

        /// <summary>
        /// Изменить запись
        /// </summary>
        protected void Edit(ServiceCounter item)
        {
            serviceCounter = item;
            modal.Open();
            dateCount = item.DateCount;
            valueCounter = item.ValueCounter;
            service = item.Service;
        }

        /// <summary>
        /// Удалить запись
        /// </summary>
        /// <param name="item"></param>
        protected void Remove(ServiceCounter item)
        {
            Repository.Remove(item);
        }


        #endregion
    }
}
