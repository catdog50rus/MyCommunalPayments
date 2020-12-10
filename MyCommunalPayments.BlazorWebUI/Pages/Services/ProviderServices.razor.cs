using Microsoft.AspNetCore.Components;
using MyCommunalPayments.BlazorWebUI.Components;
using MyCommunalPayments.Data.Services.ApiServices;
using MyCommunalPayments.Data.Services.Repositories.Base;
using MyCommunalPayments.Models.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyCommunalPayments.BlazorWebUI.Pages.Services.Base
{
    public class ProviderServicesBase : ComponentBase
    {
        #region Поля, Инициализация формы, Модальное окно

        public ProviderServiceViewModel ProviderServiceModel { get; set; }
        public ProviderServicesBase()
        {
            ProviderServiceModel = new ProviderServiceViewModel();
        }

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

        protected List<Service> services;
        private int IdService;

        //Модальное окно
        protected Modal modal;
        private bool isUpdate = default;
        protected string modalLabel = "";

        protected void CloseModal()
        {
            providersServices = default;
            isUpdate = false;
            modal.Close();
        }
        protected void OpenModal()
        {
            if (isUpdate)
            {
                modalLabel = "Изменение услуги ЖКХ у поставщика";
            }
            else
            {
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
        protected async Task AddAsync()
        {
            IdService = int.Parse(ProviderServiceModel.IdService);
            if (providersServices == null)
            {
                providersServices = new ProvidersServices()
                {

                    IdProvider = Provider.IdProvider,
                    IdService = IdService
                };

                await Repository.AddAsync(providersServices);
            }
            else
            {
                providersServices.IdService = IdService;
                await Repository.EditAsync(providersServices);

            }
            CloseModal();
            await StateUpdate();
        }

        /// <summary>
        /// Изменить запись
        /// </summary>
        protected void Edit(ProvidersServices item)
        {
            isUpdate = true;
            providersServices = item;
            OpenModal();
            ProviderServiceModel.IdService = providersServices.IdService.ToString();
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

    }

    public class ProviderServiceViewModel
    {
        [Required]
        public string IdService { get; set; } = "";
    }
}
