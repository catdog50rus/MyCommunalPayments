using Microsoft.AspNetCore.Components;
using MyCommunalPayments.BlazorWebUI.Shared;
using MyCommunalPayments.Data.Services.ApiServices;
using MyCommunalPayments.Models.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MyCommunalPayments.BlazorWebUI.Pages.Services.Base
{
    public class ServicesBase : ComponentBase
    {
        #region Поля, Инициализация формы, Модальное окно
        [Inject]
        public IApiRepository<Service> Repository { get; set; }

        protected Service service;
        protected IEnumerable<Service> Services { get; set; } = new List<Service>();

        protected string serviceName;
        protected bool count;

        //Модальное окно
        protected Modal modal;
        protected string modalTitle = "Добавление новой услуги ЖКХ";

        protected void CloseModal()
        {
            count = default;
            serviceName = default;
            service = default;
            modal.Close();
            
        }

        protected void OpenModal()
        {
            modal.Open();
        }

        protected override async Task OnInitializedAsync()
        {
            await StateUpdate();
            NavMenu.SetSubMenu(true);
            service = default;
            
        }

        #endregion

        #region Обработка нажатия кнопок

        /// <summary>
        /// Добавить дело
        /// </summary>
        protected async Task Add()
        {
            if (!string.IsNullOrWhiteSpace(serviceName))
            {

                if (service == null)
                {
                    service = new Service()
                    {
                        NameService = serviceName,
                        IsCounter = count
                    };

                    //Repository.Add(service);
                    await Repository.AddAsync(service);
                }
                else
                {
                    service.NameService = serviceName;
                    service.IsCounter = count;
                    await Repository.EditAsync(service);
                }
            }
            
            CloseModal();
            await StateUpdate();
        }

        /// <summary>
        /// Изменить запись
        /// </summary>
        protected void Edit(Service item)
        {
            service = item;
            serviceName = service.NameService;
            count = service.IsCounter;
            OpenModal();
        }

        /// <summary>
        /// Удалить запись
        /// </summary>
        protected async Task Remove(Service item)
        {
            await Repository.RemoveAsync(item.IdService);
            await StateUpdate();
        }

        #endregion

        private async Task StateUpdate()
        {
            Services = await Repository.GetAllAsync();
        }
    }
}
