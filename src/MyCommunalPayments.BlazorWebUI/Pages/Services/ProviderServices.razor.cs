using Microsoft.AspNetCore.Components;
using MyCommunalPayments.BlazorWebUI.Components;
using MyCommunalPayments.Models.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using MyCommunalPayments.UI.ApiServices;

namespace MyCommunalPayments.BlazorWebUI.Pages.Services.Base
{
    public class ProviderServicesBase : ComponentBase
    {
        #region Поля, Инициализация формы, Модальное окно

        /// <summary>
        /// Модель представления
        /// </summary>
        protected ProviderServiceViewModel ProviderServiceModel = new ProviderServiceViewModel();

        [Parameter]
        public Provider Provider { get; set; }
        [Parameter]
        public EventCallback OnClickReturnToProviders { get; set; }

        [Inject]
        public IApiRepository<ProvidersServices> Repository { get; set; }
        [Inject]
        public IApiRepository<Service> RepositoryServices { get; set; }

        //Услуги поставщиков
        protected ProvidersServices providersServices;
        protected IEnumerable<ProvidersServices> providersServicesCollection;

        //Услуги
        protected List<Service> services;

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
            //Получаем id услуги
            int idService = int.Parse(ProviderServiceModel.IdService);

            //Проверяем, существует ли текущая модель 
            if (providersServices == null)
            {
                //Создаем и инициализируем модель
                providersServices = new ProvidersServices()
                {
                    IdProvider = Provider.IdProvider,
                    IdService = idService
                };
                //Записываем модель в БД
                await Repository.AddAsync(providersServices);
            }
            else
            {
                //Меняем модель и записываем изменения в БД
                providersServices.IdService = idService;
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
            //Готовим модель представления
            isUpdate = true;
            providersServices = item;
            OpenModal();
            ProviderServiceModel.IdService = providersServices.IdService.ToString();
        }

        /// <summary>
        /// Удалить запись
        /// </summary>
        /// <param name="item"></param>
        protected async Task RemoveAsync(ProvidersServices item)
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

    /// <summary>
    /// Модель представления
    /// </summary>
    public class ProviderServiceViewModel
    {
        /// <summary>
        /// Услуга
        /// </summary>
        [Required]
        public string IdService { get; set; } = "";
    }
}
