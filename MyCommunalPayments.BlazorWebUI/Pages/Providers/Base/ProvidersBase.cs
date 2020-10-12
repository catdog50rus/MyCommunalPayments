using Microsoft.AspNetCore.Components;
using MyCommunalPayments.BlazorWebUI.Shared;
using MyCommunalPayments.Data.Services.Repositories.Base;
using MyCommunalPayments.Data.Services.Toast;
using MyCommunalPayments.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyCommunalPayments.BlazorWebUI.Pages.Providers
{
    public class ProvidersBase : ComponentBase
    {
        #region Поля, Инициализация формы, Модальное окно


        [Inject]
        public IRepository<Provider> Repository { get; set; }

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

        protected override void OnInitialized()
        {
            providers = new List<Provider>();
            providers = Repository.GetAll();
            NavMenu.SetSubMenu(true);
        }

        #endregion

        #region Обработка нажатия кнопок

        /// <summary>
        /// Добавить или отредактировать
        /// </summary>
        protected void Add()
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

                    Repository.Add(provider);
                }
                else
                {
                    provider.NameProvider = provideName;
                    provider.WebSite = webSite;
                    Repository.Edit(provider);
                }
            }

            CloseModal();
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

        protected void Confirm()
        {
            confirm = true;

            DeleteData();
        }

        protected void DeleteData()
        {

            if (confirm && provider != null)
            {
                Repository.Remove(provider);
            }
            confirm = false;

        }

        #endregion
    }
}
