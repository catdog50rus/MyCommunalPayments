using Microsoft.AspNetCore.Components;
using MyCommunalPayments.Data.Services.ApiServices;
using MyCommunalPayments.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyCommunalPayments.BlazorWebUI.Pages.Services.Base
{
    public class ServicesCountersBase : ComponentBase
    {
        #region Поля, Инициализация формы, Модальное окно

        [Inject]
        public IApiRepository<ServiceCounter> Repository { get; set; }

        [Inject]
        public IApiRepository<Service> ServiceRepository { get; set; }

        protected IEnumerable<ServiceCounter> serviceCounters;
        protected ServiceCounter serviceCounter;
        protected string dateCount;
        protected int valueCounter;

        protected List<Service> services;
        private int serviceId;
        protected string serviceName = "";

        //Модальное окно
        protected Modal modal;

        protected void CloseModal()
        {
            serviceCounter = default;
            dateCount = default;
            valueCounter = default;
            modal.Close();
        }
        protected void OpenModal()
        {
            dateCount = DateTime.Today.ToString("dd/MM/yyyy");
            modal.ModalSize = "modal-lg";
            modal.Open();
        }

        protected override async Task OnInitializedAsync()
        {
            await StateUpdate();
            services = (await ServiceRepository.GetAllAsync()).ToList();
            services = services.Where(c => c.IsCounter == true).ToList();
        }

        protected Service GetServiceByName(string name)
        {
            return services.FirstOrDefault(s => s.NameService == name);
        }


        #endregion

        #region Обработка нажатия кнопок

        /// <summary>
        /// Добавить или отредактировать
        /// </summary>
        protected async Task Add()
        {
            if (!string.IsNullOrWhiteSpace(dateCount) && !string.IsNullOrWhiteSpace(serviceName) && valueCounter >= 0)
            {
                serviceId = GetServiceByName(serviceName).IdService;

                if (serviceCounter == null)
                {
                    serviceCounter = new ServiceCounter()
                    {
                        DateCount = dateCount,
                        ValueCounter = valueCounter,
                        IdService = serviceId
                    };

                    await Repository.AddAsync(serviceCounter);
                }
                else
                {
                    serviceCounter.DateCount = dateCount;
                    serviceCounter.IdService = serviceId;
                    serviceCounter.ValueCounter = valueCounter;
                    await Repository.EditAsync(serviceCounter);
                }
            }

            CloseModal();
            await StateUpdate();
        }

        /// <summary>
        /// Изменить запись
        /// </summary>
        protected void Edit(ServiceCounter item)
        {
            serviceCounter = item;
            serviceName = item.Service.NameService;
            dateCount = item.DateCount;
            valueCounter = item.ValueCounter;
            OpenModal();
        }

        /// <summary>
        /// Удалить запись
        /// </summary>
        /// <param name="item"></param>
        protected async Task Remove(ServiceCounter item)
        {
            await Repository.RemoveAsync(item.IdCounter);
            await StateUpdate();
        }

        private async Task StateUpdate()
        {
            serviceCounters = await Repository.GetAllAsync();
            serviceCounters.OrderByDescending(d => d.ToSort());//.ThenBy(s => s.Service.NameService);
            
        }

        #endregion
    }
}
