using Microsoft.AspNetCore.Components;
using MyCommunalPayments.BlazorWebUI.Components;
using MyCommunalPayments.Data.Services.ApiServices;
using MyCommunalPayments.Data.Services.Repositories.Base;
using MyCommunalPayments.Models.Models;
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
        public IApiRepository<ProvidersServices> Repository { get; set; }
        [Inject]
        public IApiRepository<Service> RepositoryServices { get; set; }

        protected ProvidersServices providersServices;
        protected IEnumerable<ProvidersServices> providersServicesCollection;

        protected string serviceName = "";

        protected List<Service> services;
        protected Service service;

        //Модальное окно
        protected Modal modal;// { get; set; }

        private bool isUpdate = default;
        protected string buttonLabel = "";
        protected string modalLabel = "";

        protected async Task CloseModal()
        {
            serviceName = "";
            providersServices = default;
            isUpdate = false;
            modal.Close();
            await StateUpdate();
        }
        protected void OpenModal()
        {
            if (isUpdate)
            {
                buttonLabel = "Изменить";
                modalLabel = "Изменение услуги ЖКХ у поставщика";
            }
            else
            {
                buttonLabel = "Добавить";
                modalLabel = "Добавление услуг ЖКХ поставщику";
            }
            modal.Open();
        }

        protected override async Task OnInitializedAsync()
        {
            services = (await RepositoryServices.GetAllAsync()).ToList();
            await StateUpdate();
        }


        #endregion

        #region Обработка нажатия кнопок

        /// <summary>
        /// Добавить или отредактировать
        /// </summary>
        protected async Task Add()
        {
            if (!string.IsNullOrWhiteSpace(serviceName))
            {
                service = GetServiceByName(serviceName);
                if (providersServices == null)
                {
                    providersServices = new ProvidersServices()
                    {
                        
                        IdProvider = Provider.IdProvider,
                        IdService = service.IdService,
                    };

                    await Repository.AddAsync(providersServices);
                }
                else
                {
                    providersServices.IdService = service.IdService;
                    await Repository.EditAsync(providersServices);
                    await CloseModal();
                }
            }
            providersServices = default;
            
        }

        /// <summary>
        /// Изменить запись
        /// </summary>
        protected void Edit(ProvidersServices item)
        {
            isUpdate = true;
            providersServices = item;
            OpenModal();
            serviceName = item.Service.NameService;
        }

        /// <summary>
        /// Удалить запись
        /// </summary>
        /// <param name="item"></param>
        protected async Task Remove(ProvidersServices item)
        {
            await Repository.RemoveAsync(item.Id);
            await StateUpdate();
        }

        #endregion

        private async Task StateUpdate()
        {
            providersServicesCollection = await Repository.GetAllAsync();
            providersServicesCollection = providersServicesCollection
                .Where(p => p.Provider.IdProvider == Provider.IdProvider).ToList();
        }

        private Service GetServiceByName(string name) => services.Single(i => i.NameService == name);

    }
}
