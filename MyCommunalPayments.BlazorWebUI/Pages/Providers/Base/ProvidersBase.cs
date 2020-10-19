using Microsoft.AspNetCore.Components;
using MyCommunalPayments.BlazorWebUI.Shared;
using MyCommunalPayments.Data.Services.ApiServices;
using MyCommunalPayments.Data.Services.Toast;
using MyCommunalPayments.Models.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyCommunalPayments.BlazorWebUI.Pages.Providers
{
    public class ProvidersBase : ComponentBase
    {
        #region Поля, Инициализация формы, Модальное окно


        [Inject]
        public IApiRepository<Provider> Repository { get; set; }

        protected Provider provider = default;
        protected IEnumerable<Provider> providers;
        protected string provideName;
        protected string webSite;

        protected bool isProvider = true;

        protected bool confirm = false;


        //Модальное окно
        protected Modal modal;// { get; set; }
        protected void CloseModal()
        {
            provideName = default;
            webSite = default;
            provider = default;
            modal.Close();
        }
        protected void OpenModal()
        {
            modal.Open();
        }

        protected Toast toast;// { get; set; }
        protected string message;

        protected Modal setServicesModal;// { get; set; }


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
        protected async Task Add()
        {
            if (!string.IsNullOrWhiteSpace(provideName) || !string.IsNullOrWhiteSpace(provider.NameProvider))
            {

                if (provider == null)
                {
                    provider = new Provider()
                    {
                        NameProvider = provideName,
                        WebSite = webSite
                    };

                    await Repository.AddAsync(provider);
                }
                else
                {
                    provider.NameProvider = provideName;
                    provider.WebSite = webSite;
                    await Repository.EditAsync(provider);
                }
            }

            CloseModal();
            await StateUpdate();
            ToastShow("Данные обновлены", ToastLevel.Success);
        }

        /// <summary>
        /// Изменить запись
        /// </summary>
        protected void Edit(Provider item)
        {
            provider = item;
            modal.Open();
            provideName = item.NameProvider;
            webSite = item.WebSite;
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

        protected async Task DeleteData()
        {

            if (confirm && provider != null)
            {
                await Repository.RemoveAsync(provider.IdProvider);
                await StateUpdate();
            }
            confirm = false;

        }

        #endregion

        private async Task StateUpdate()
        {
            providers = await Repository.GetAllAsync();
        }
    }
}
