using Microsoft.AspNetCore.Components;
using MyCommunalPayments.BlazorWebUI.Components;
using MyCommunalPayments.BlazorWebUI.Shared;
using MyCommunalPayments.Models.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using MyCommunalPayments.UI.ApiServices;
using ToastLevel = MyCommunalPayments.BlazorWebUI.Services.Toast.ToastLevel;

namespace MyCommunalPayments.BlazorWebUI.Pages.Providers
{
    public class ProvidersBase : ComponentBase
    {
        #region Поля, Инициализация формы, Модальное окно

        /// <summary>
        /// Модель представления
        /// </summary>
        protected ProviderViewModel ProviderModel = new ProviderViewModel();

        [Inject]
        public IApiRepository<Provider> Repository { get; set; }

        //Поставщик
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

            //Проверяем есть ли текущая модель
            if (provider == null)
            {
                //Создаем и инициализируем модель
                provider = new Provider()
                {
                    NameProvider = ProviderModel.NameProvider,
                    WebSite = ProviderModel.WebSite
                };
                //Если модель уникальная, записываем в БД
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
                //Изменяем модель
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

        /// <summary>
        /// Устанавливаем услуги для каждого поставщика
        /// </summary>
        /// <param name="item"></param>
        protected void SetServices(Provider item)
        {
            provider = item;
            isProvider = false;
        }

        /// <summary>
        /// Возвращаемся к интерфейсу списка поставщиков
        /// </summary>
        protected void ReturnFromService() => isProvider = true;


        #endregion

        protected void ToastShow(string mes, ToastLevel level)
        {
            message = mes;
            toast.ShowToast(level);
        }

        /// <summary>
        /// Подтверждение удаления
        /// </summary>
        /// <returns></returns>
        protected async Task Confirm()
        {
            confirm = true;

            await DeleteData();
        }

        /// <summary>
        /// Реализация удаления
        /// </summary>
        /// <returns></returns>
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

    /// <summary>
    /// Модель представления
    /// </summary>
    public class ProviderViewModel
    {
        /// <summary>
        /// Наименование поставщика услуги ЖКХ
        /// </summary>
        [Required]
        [MinLength(5, ErrorMessage = "Название не может быть короче 5-ти символов")]
        [MaxLength(70, ErrorMessage = "Название не может быть длиннее 70-ти символов")]
        public string NameProvider { get; set; }

        /// <summary>
        /// Путь к личному кабинету поставщика
        /// </summary>
        public string WebSite { get; set; }
    }
}
