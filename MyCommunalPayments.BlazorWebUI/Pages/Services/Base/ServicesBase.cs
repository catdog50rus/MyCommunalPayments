using Microsoft.AspNetCore.Components;
using MyCommunalPayments.BlazorWebUI.Shared;
using MyCommunalPayments.Data.Services.Repositories.Base;
using MyCommunalPayments.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyCommunalPayments.BlazorWebUI.Pages.Services.Base
{
    public class ServicesBase : ComponentBase
    {
        #region Поля, Инициализация формы, Модальное окно
        [Inject]
        public IRepository<Service> Repository { get; set; }

        protected Service service;
        protected IEnumerable<Service> services;
        protected string serviceName;
        protected bool count;

        //Модальное окно
        protected Modal modal;// { get; set; }

        protected void CloseModal()
        {
            count = default;
            serviceName = default;
            modal.Close();
        }

        protected void OpenModal()
        {
            Message = default;
            modal.Open();
        }

        //Уведомление об операции
        protected string Message { get; set; }


        protected override void OnInitialized()
        {
            //services = new List<Service>();
            services = Repository.GetAll().ToList();
            NavMenu.SetSubMenu(true);
        }

        #endregion

        #region Обработка нажатия кнопок

        /// <summary>
        /// Добавить дело
        /// </summary>
        protected void Add()
        {
            string message = default;
            if (!string.IsNullOrWhiteSpace(serviceName))
            {

                if (service == null)
                {
                    service = new Service()
                    {
                        NameService = serviceName,
                        IsCounter = count
                    };

                    Repository.Add(service);

                    message = $"Услуга добавлена!";


                }
                else
                {
                    service.NameService = serviceName;
                    service.IsCounter = count;
                    Repository.Edit(service);
                    message = $"Услуга обновлена!";
                }
            }

            Message = message;
            CloseModal();
        }

        /// <summary>
        /// Изменить запись
        /// </summary>
        protected void Edit(Service item)
        {
            service = item;
            OpenModal();
            serviceName = service.NameService;
            count = service.IsCounter;

        }

        /// <summary>
        /// Удалить запись
        /// </summary>
        protected void Remove(Service item)
        {
            Repository.Remove(item);
        }

        #endregion
    }
}
