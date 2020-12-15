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
        public InvoiceViewModel InvoiceModel { get; set; }
        public InvoicesBase()
        {
            InvoiceModel = new InvoiceViewModel();
        }

        [Parameter]
        public IApiRepository<Invoice> Repository { get; set; }

        [Parameter]
        public List<Provider> Providers { get; set; }

        [Parameter]
        public List<Period> Periods { get; set; }

        [Parameter]
        public EventCallback<Invoice> OnClickSetService { get; set; }

        protected IEnumerable<Invoice> invoices;
        protected Invoice invoice;
        protected decimal summ;
        protected bool pay;
        protected bool isNotPaided = true;
        protected bool isPay;

        //Providers
        protected Provider provider;
        protected List<Provider> providersList;
        protected int IdProvider;

        //Periods
        protected Period period;
        protected List<Period> periodsList;
        protected int IdPeriod;

        //Модальное окно
        protected Modal modal;

        protected void CloseModal()
        {
            invoice = default;
            pay = default;
            modal.ModalSize = "";
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
            pay = default;
            
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
            IdProvider = int.Parse(InvoiceModel.IdProvider);
            IdPeriod = int.Parse(InvoiceModel.IdPeriod);
            //Добавить новый
            if (invoice == null)
            {
                invoice = new Invoice()
                {
                    IdPeriod = IdPeriod,
                    IdProvider = IdProvider,
                    InvoiceSum = InvoiceModel.InvoiceSum,
                    Pay = false
                };

                if(invoices.FirstOrDefault(i=>i.Equals(invoice)) == null)
                {
                    await Repository.AddAsync(invoice);
                }
                

            }
            //Отредактировать
            else
            {
                invoice.IdPeriod = IdPeriod;
                invoice.IdProvider = IdProvider;
                invoice.InvoiceSum = InvoiceModel.InvoiceSum;
                invoice.Pay = pay;
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
            invoice = item;
            OpenModal();

            InvoiceModel.IdPeriod = invoice.IdPeriod.ToString();
            InvoiceModel.IdProvider = invoice.IdProvider.ToString();
            InvoiceModel.InvoiceSum = invoice.InvoiceSum;
            pay = invoice.Pay;

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

        protected void SetService(Invoice item)
        {
            OnClickSetService.InvokeAsync(item);
        }

        protected void Pay(Invoice item)
        {
            invoice = item;
            isPay = true;
        }

        protected async Task ShowPaided()
        {
            isNotPaided = !isNotPaided;
            await StateUpdate(isNotPaided);
        }

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

        protected string GetWeb(int id) => Providers.FirstOrDefault(p => p.IdProvider == id).WebSite;
    }

    public class InvoiceViewModel
    {
        [Required(ErrorMessage = "Необходимо выбрать период!")]
        public string IdPeriod { get; set; } = "";
        [Required(ErrorMessage = "Необходимо выбрать поставщика!")]
        public string IdProvider { get; set; } = "";
        [Required(ErrorMessage = "Необходимо указать сумму квитанции!")]
        [Range(0, 100000, ErrorMessage = "Сумма должна быть неотрицательным числом!")]
        public decimal InvoiceSum { get; set; }
    }
}
