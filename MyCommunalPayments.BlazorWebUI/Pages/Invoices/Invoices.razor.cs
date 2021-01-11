using Microsoft.AspNetCore.Components;
using MyCommunalPayments.BlazorWebUI.Components;
using MyCommunalPayments.Data.Services.ApiServices;
using MyCommunalPayments.Models.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyCommunalPayments.BlazorWebUI.Pages.Invoices
{
    public class InvoicesBase : ComponentBase
    {
        #region Поля, Инициализация формы, Модальное окно

        /// <summary>
        /// Модель представления
        /// </summary>
        protected InvoiceViewModel InvoiceViewModel = new InvoiceViewModel();

        [Parameter]
        public IApiRepository<Invoice> Repository { get; set; }

        [Parameter]
        public List<Provider> Providers { get; set; }

        [Parameter]
        public List<Period> Periods { get; set; }

        [Parameter]
        public EventCallback<Invoice> OnClickSetService { get; set; }

        /// <summary>
        /// Список квитанций
        /// </summary>
        protected IEnumerable<Invoice> invoices;
        /// <summary>
        /// Квитанция
        /// </summary>
        protected Invoice invoice;

        //protected bool pay;
        protected bool isNotPaided = true;
        protected bool isPay;

        //Providers
        protected Provider provider;
        protected List<Provider> providersList;
        protected int IdProvider;

        //Periods
        protected Period period;
        //protected List<Period> periodsList;
        protected int IdPeriod;

        //Модальное окно
        protected Modal modal;
        protected void CloseModal()
        {
            invoice = default;
            modal.Close(); 
        }
        protected void OpenModal()
        {
            modal.ModalSize = "modal-lg";
            modal.Open();
        }

        protected override async Task OnInitializedAsync()
        {
            await StateUpdate(isNotPaided);
            invoice = default;
            isPay = default;
        }

        #endregion

        #region Обработка нажатия кнопок

        /// <summary>
        /// Добавить или отредактировать
        /// </summary>
        protected async Task AddAsync()
        {
            //Получаем из модели представления id
            IdProvider = int.Parse(InvoiceViewModel.IdProvider);
            IdPeriod = int.Parse(InvoiceViewModel.IdPeriod);
            
            //Проверяем, есть ли текущая квитанция
            if (invoice == null)
            {
                //Создаем и инициализируем экземпляр квитанции
                invoice = new Invoice()
                {
                    IdPeriod = IdPeriod,
                    IdProvider = IdProvider,
                    InvoiceSum = InvoiceViewModel.InvoiceSum,
                };

                //Если квитанция уникальная записываем ее в БД
                if(invoices.FirstOrDefault(i=>i.Equals(invoice)) == null)
                {
                    await Repository.AddAsync(invoice);
                }
            }
            else
            {
                //Меняем модель и вносим изменения в БД
                invoice.IdPeriod = IdPeriod;
                invoice.IdProvider = IdProvider;
                invoice.InvoiceSum = InvoiceViewModel.InvoiceSum;

                await Repository.EditAsync(invoice);
            }

            await StateUpdate(isNotPaided);

            CloseModal();
        }

        /// <summary>
        /// Изменить запись
        /// </summary>
        protected void Edit(Invoice item)
        {
            //Готовим модель представления
            invoice = item;
            OpenModal();

            InvoiceViewModel.IdPeriod = invoice.IdPeriod.ToString();
            InvoiceViewModel.IdProvider = invoice.IdProvider.ToString();
            InvoiceViewModel.InvoiceSum = invoice.InvoiceSum;
        }

        /// <summary>
        /// Удалить запись
        /// </summary>
        /// <param name="item"></param>
        protected async Task Remove(Invoice item)
        {
            await Repository.RemoveAsync(item.IdInvoice);
            await StateUpdate(isNotPaided);
        }

        /// <summary>
        /// Устанавливаем в квитанцию услуги
        /// </summary>
        /// <param name="item"></param>
        protected void SetService(Invoice item)
        {
            OnClickSetService.InvokeAsync(item);
        }

        /// <summary>
        /// Переходим к оплате квитанции
        /// </summary>
        /// <param name="item"></param>
        protected void Pay(Invoice item)
        {
            invoice = item;
            isPay = true;
        }

        /// <summary>
        /// Скрыть / Показать оплаченные квитанции
        /// </summary>
        /// <returns></returns>
        protected async Task ShowPaided()
        {
            isNotPaided = !isNotPaided;
            await StateUpdate(isNotPaided);
        }

        /// <summary>
        /// Возврат к интерфейсу списка квитанций после оплаты
        /// </summary>
        protected void ReturnToPayment()
        {
            isPay = false;
        }

        #endregion

        private async Task StateUpdate(bool show)
        {
            if (show)
            {
                invoices = (await Repository.GetAllAsync()).ToList().Where(i => i.Pay == false).OrderByDescending(p => p.Period.ToSort());
            }
            else
            {
                invoices = (await Repository.GetAllAsync()).ToList().OrderByDescending(p => p.Period.ToSort());
            }
        }

        /// <summary>
        /// Получить URL к личному кабинету на сайте поставщика услуг
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        protected string GetWeb(int id) => Providers.FirstOrDefault(p => p.IdProvider == id).WebSite;
    }

    /// <summary>
    /// Модель представления
    /// </summary>
    public class InvoiceViewModel
    {
        /// <summary>
        /// Период
        /// </summary>
        [Required(ErrorMessage = "Необходимо выбрать период!")]
        public string IdPeriod { get; set; } = "";

        /// <summary>
        /// Поставщик услуг
        /// </summary>
        [Required(ErrorMessage = "Необходимо выбрать поставщика!")]
        public string IdProvider { get; set; } = "";

        /// <summary>
        /// Сумма квитанции
        /// </summary>
        [Required(ErrorMessage = "Необходимо указать сумму квитанции!")]
        [Range(0, 100000, ErrorMessage = "Сумма должна быть неотрицательным числом!")]
        public decimal InvoiceSum { get; set; }
    }
}
