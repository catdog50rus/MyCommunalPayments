using Microsoft.AspNetCore.Components;
using MyCommunalPayments.Data.Services.Repositories.Base;
using MyCommunalPayments.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyCommunalPayments.BlazorWebUI.Pages.Services.Base
{
    public class ProviderServicesBase : ComponentBase
    {
        #region Поля, Инициализация формы, Модальное окно

        [Parameter]
        public Provider Provider { get; set; }
        [Parameter]
        public EventCallback OnClickReturnToProviders { get; set; }

        [Inject]
        public IRepository<ProvidersServices> Repository { get; set; }
        [Inject]
        public IRepository<Service> RepositoryServices { get; set; }

        protected ProvidersServices providersServices = default;
        protected IEnumerable<ProvidersServices> ProvidersServicesCollection;

        protected Provider provider;
        protected string serviceName;

        protected List<Service> services;

        //Модальное окно
        protected Modal modal;// { get; set; }

        protected void CloseModal()
        {
            serviceName = default;
            providersServices = default;
            modal.Close();
        }
        protected void OpenModal()
        {
            modal.Open();
        }

        protected override void OnInitialized()
        {
            ProvidersServicesCollection = Repository.GetAll().Where(p => p.Provider.IdProvider == Provider.IdProvider);

            provider = Provider;
            services = RepositoryServices.GetAll().ToList();
            serviceName = services[0].NameService;
        }

        protected Service GetServiceByName(string name) => services.Single(i => i.NameService == name);



        #endregion

        #region Обработка нажатия кнопок

        /// <summary>
        /// Добавить или отредактировать
        /// </summary>
        protected void Add()
        {
            if (provider != null && !string.IsNullOrWhiteSpace(serviceName))
            {

                if (providersServices == null)
                {
                    providersServices = new ProvidersServices()
                    {
                        Provider = provider,
                        Service = GetServiceByName(serviceName)
                    };

                    Repository.Add(providersServices);
                }
                else
                {
                    providersServices.Service = GetServiceByName(serviceName);
                    Repository.Edit(providersServices);
                }
            }
            providersServices = default;
        }

        /// <summary>
        /// Изменить запись
        /// </summary>
        protected void Edit(ProvidersServices item)
        {
            providersServices = item;
            modal.Open();
            serviceName = item.Service.NameService;
        }

        /// <summary>
        /// Удалить запись
        /// </summary>
        /// <param name="item"></param>
        protected void Remove(ProvidersServices item)
        {
            Repository.Remove(item);
        }

        #endregion

    }
}
