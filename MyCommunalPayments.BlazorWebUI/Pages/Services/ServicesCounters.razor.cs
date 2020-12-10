using Microsoft.AspNetCore.Components;
using MyCommunalPayments.BlazorWebUI.Components;
using MyCommunalPayments.Data.Services.ApiServices;
using MyCommunalPayments.Models.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyCommunalPayments.BlazorWebUI.Pages.Services.Base 
{ 
    public class ServicesCountersBase : ComponentBase
    {
        #region Поля, Инициализация формы, Модальное окно

        public ServiceCounterViewModel ServiceCounterModel { get; set; }
        public ServicesCountersBase()
        {
            ServiceCounterModel = new ServiceCounterViewModel();
        }

        [Inject]
        public IApiRepository<ServiceCounter> Repository { get; set; }

        [Inject]
        public IApiRepository<Service> ServiceRepository { get; set; }

        protected IEnumerable<ServiceCounter> serviceCounters;
        protected ServiceCounter serviceCounter;

        protected List<Service> services;
        private int serviceId;
        private string countDate;

        //Модальное окно
        protected Modal modal;

        protected void CloseModal()
        {
            serviceCounter = default;
            ServiceCounterModel = new ServiceCounterViewModel();
            modal.Close();
        }
        protected void OpenModal()
        {
            modal.ModalSize = "modal-lg";
            modal.Open();
        }

        protected override async Task OnInitializedAsync()
        {
            await StateUpdate();
            services = (await ServiceRepository.GetAllAsync()).ToList();
            services = services.Where(c => c.IsCounter == true).ToList();
        }

        #endregion

        #region Обработка нажатия кнопок

        /// <summary>
        /// Добавить или отредактировать
        /// </summary>
        protected async Task AddAsync()
        {

            serviceId = int.Parse(ServiceCounterModel.ServiceId);
            countDate = ServiceCounterModel.DateCount.ToString("dd/MM/yyyy");

                if (serviceCounter == null)
                {
                    serviceCounter = new ServiceCounter()
                    {
                        DateCount = countDate,
                        ValueCounter = ServiceCounterModel.ValueCounter,
                        IdService = serviceId
                    };

                    await Repository.AddAsync(serviceCounter);
                }
                else
                {
                    serviceCounter.DateCount = countDate;
                    serviceCounter.IdService = serviceId;
                    serviceCounter.ValueCounter = ServiceCounterModel.ValueCounter;
                    await Repository.EditAsync(serviceCounter);
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
            ServiceCounterModel = new ServiceCounterViewModel()
            {
                ServiceId = serviceCounter.Service.IdService.ToString(),
                DateCount = DateTime.Parse(serviceCounter.DateCount),
                ValueCounter = serviceCounter.ValueCounter
            };
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
            serviceCounters =  await Repository.GetAllAsync();
            _ = serviceCounters.OrderByDescending(d => d.ToSort()).ThenBy(s => s.Service.NameService);
            
        }

        #endregion
    }

    public class ServiceCounterViewModel
    {
        [Required(ErrorMessage = "Необходимо выбрать услугу")]
        public string ServiceId { get; set; } = "";
        [Required(ErrorMessage = "Необходимо указать дату")]
        public DateTime DateCount { get; set; } = DateTime.Today;
        [Required(ErrorMessage = "Поле должно быть заполнено!")]
        [Range(0, 100000, ErrorMessage = "Показания счетчика - не отрицательное число")]
        public int ValueCounter { get; set; }
    }
}
