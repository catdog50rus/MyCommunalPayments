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
    public class InvoicesServicesBase : ComponentBase
    {
        #region Поля, Инициализация формы, Модальное окно

        /// <summary>
        /// Модель представления
        /// </summary>
        protected InvoiceServiceViewModel InvoiceServiceModel = new InvoiceServiceViewModel();

        [Parameter]
        public Invoice Invoice { get; set; }
        [Parameter]
        public EventCallback OnClickReturnToInvoces { get; set; }

        [Inject]
        public IApiRepository<InvoiceServices> Repository { get; set; }
        [Inject]
        public IApiRepository<ServiceCounter> CountersRepository { get; set; }
        [Inject]
        public IApiRepository<ProvidersServices> ProviderServicesRepository { get; set; }

        
        //Сервисы в квитанции
        protected InvoiceServices invoiceService;
        protected IEnumerable<InvoiceServices> invoiceServicesList;

        //Поставщик
        protected Provider provider;

        //Сервисы
        protected Service service;
        protected List<Service> services;

        //Счетчики услуг
        protected ServiceCounter counter;
        protected List<ServiceCounter> counters;
        protected string dateCount;

        //Модальное окно
        protected Modal modal;
        protected void CloseModal()
        {
            modal.Close();
        }
        protected void OpenModal()
        {
            modal.Open();
        }

        protected override async Task OnInitializedAsync()
        {
            
            if (Invoice != null) 
            {
                await StateUpdate();
                provider = Invoice.Provider;
                var pservices = await ProviderServicesRepository.GetAllAsync();
                services = pservices.Where(p => p.Provider.IdProvider == provider.IdProvider).Select(s => s.Service).ToList();
            }
            counters = (await CountersRepository.GetAllAsync()).ToList();
        }



        #endregion

        #region Обработка нажатия кнопок

        /// <summary>
        /// Добавить дело
        /// </summary>
        protected async Task AddAsync()
        {
            //Получаем id сервиса
            int idService = int.Parse(InvoiceServiceModel.IdService);
            //Последнее показание счетчика
            int amount = 0;

            //Получаем сервис по id и проверяем его на null
            service = services.FirstOrDefault(s => s.IdService == idService);
            if(service != null)
            {
                //Если сервис подразумевает наличие счетчика получаем последнее показания
                if (service.IsCounter)
                {
                    amount = counters.Where(s => s.IdService == idService).Select(c => c.ValueCounter).Max();
                }

                //Проверяем если ли текущая модель
                if (invoiceService == null)
                {
                    //Создаем и инициализируем модель
                    invoiceService = new InvoiceServices()
                    {
                        IdInvoice = Invoice.IdInvoice,
                        IdService = idService,
                        Amount = amount
                    };
                    //Если модель уникальная, записываем ее в БД
                    if(invoiceServicesList.FirstOrDefault(ins=>ins.Equals(invoiceService)) == null)
                    {
                        await Repository.AddAsync(invoiceService);
                    }
                }
                else
                {
                    //Меняем Модель
                    invoiceService.IdService = idService;
                    await Repository.EditAsync(invoiceService);
                }
            }
            
            invoiceService = default;
            service = default;
            await StateUpdate();
            CloseModal();

        }

        /// <summary>
        /// Изменить запись
        /// </summary>
        protected void Edit(InvoiceServices item)
        {
            //Готовим модель представления
            invoiceService = item;
            OpenModal();
            InvoiceServiceModel.IdService = invoiceService.Invoice.ToString();
        }

        /// <summary>
        /// Удалить запись
        /// </summary>
        protected async Task RemoveAsync(InvoiceServices item)
        {
            await Repository.RemoveAsync(item.IdInvoiceServices);
            await StateUpdate();
        }


        #endregion

        private async Task StateUpdate()
        {
            invoiceServicesList = await Repository.GetAllAsync();
            invoiceServicesList = invoiceServicesList
                .Where(p => p.Invoice.IdInvoice == Invoice.IdInvoice);
        }
    }

    /// <summary>
    /// Модель представления
    /// </summary>
    public class InvoiceServiceViewModel
    {
        /// <summary>
        /// Получение услуги
        /// </summary>
        [Required(ErrorMessage = "Необходимо выбрать услугу")]
        public string IdService { get; set; } = "";
    }
}
