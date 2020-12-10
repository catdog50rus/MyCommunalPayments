using Microsoft.AspNetCore.Components;
using MyCommunalPayments.BlazorWebUI.Components;
using MyCommunalPayments.BlazorWebUI.Shared;
using MyCommunalPayments.Data.Services.ApiServices;
using MyCommunalPayments.Data.Services.Toast;
using MyCommunalPayments.Models.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyCommunalPayments.BlazorWebUI.Pages.Providers
{
    public class ProvidersBase : ComponentBase
    {
        #region Поля, Инициализация формы, Модальное окно

        public ProviderViewModel ProviderModel { get; set; }
        public ProvidersBase()
        {
            ProviderModel = new ProviderViewModel();
        }

        [Inject]
        public IApiRepository<Provider> Repository { get; set; }

        protected Provider provider = default;
        protected IEnumerable<Provider> providers;

        protected bool isProvider = true;


        //Модальное окно
        protected Modal modal;
        protected void CloseModal()
        {
            ProviderModel = new ProviderViewModel();

            provider = default;
            modal.Close();
        }
        protected void OpenModal()
        {
            modal.Open();
        }
        protected Modal setServicesModal;

        //Уведомление
        protected Toast toast;
        protected string message;
        protected bool confirm = false;

        


        protected override async Task OnInitializedAsync()
        {
            await StateUpdate();
            NavMenu.SetSubMenu(true);
        }


        #endregion

        #region Обработка нажатия кнопок

        /// <summary>
        /// Добавить или отредактировать
        /// </summary>
        protected async Task AddAsync()
        {
            (string, ToastLevel) toastMessage = ("Данные обновлены", ToastLevel.Success);
            if (provider == null)
            {
                provider = new Provider()
                {
                    NameProvider = ProviderModel.NameProvider,
                    WebSite = ProviderModel.WebSite
                };

                if(providers.FirstOrDefault(p => p.Equals(provider)) == null)
                {
                    await Repository.AddAsync(provider);
                }
                else
                {
                    toastMessage = ("Такой поставщик уже существует!", ToastLevel.Error);
                }
                
            }
            else
            {
                provider.NameProvider = ProviderModel.NameProvider;
                provider.WebSite = ProviderModel.WebSite;
                await Repository.EditAsync(provider);
            }


            CloseModal();
            await StateUpdate();
            ToastShow(toastMessage.Item1, toastMessage.Item2);
        }

        /// <summary>
        /// Изменить запись
        /// </summary>
        protected void Edit(Provider item)
        {
            provider = item;
            modal.Open();
            ProviderModel.NameProvider = provider.NameProvider;
            ProviderModel.WebSite = provider.WebSite;
        }

        /// <summary>
        /// Удалить запись
        /// </summary>
        /// <param name="item"></param>
        protected void Remove(Provider item)
        {
            provider = item;

            ToastShow("Внимание! Данные о поставщике будут безвозвратно удалены. Вы уверенны?", ToastLevel.Warning);
        }

        protected void SetServices(Provider item)
        {
            provider = item;
            isProvider = false;


        }

        protected void ReturnFromService() => isProvider = true;



        #endregion

        protected void ToastShow(string mes, ToastLevel level)
        {
            message = mes;
            toast.ShowToast(level);
        }

        protected async Task Confirm()
        {
            confirm = true;

            await DeleteData();
        }

        private async Task DeleteData()
        {

            if (confirm && provider != null)
            {
                await Repository.RemoveAsync(provider.IdProvider);
                await StateUpdate();
            }
            confirm = false;

        }

        private async Task StateUpdate()
        {
            providers = await Repository.GetAllAsync();
        }
    }

    public class ProviderViewModel
    {
        /// <summary>
        /// Наименование поставщика услуги ЖКХ
        /// </summary>
        [Required]
        [MinLength(5, ErrorMessage = "Слишком короткое название")]
        [MaxLength(70, ErrorMessage = "Слишком длинное название")]
        public string NameProvider { get; set; }
        /// <summary>
        /// Путь к личному кабинету поставщика
        /// </summary>
        public string WebSite { get; set; }
    }
}
