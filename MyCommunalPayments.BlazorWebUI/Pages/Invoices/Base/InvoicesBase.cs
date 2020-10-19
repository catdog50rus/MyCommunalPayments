using Microsoft.AspNetCore.Components;
using MyCommunalPayments.Data.Services.ApiServices;
using MyCommunalPayments.Models.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyCommunalPayments.BlazorWebUI.Pages.Invoices
{
    public class InvoicesBase : ComponentBase
    {
        #region Поля, Инициализация формы, Модальное окно
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
        protected string providerName;

        //Periods
        protected Period period;
        protected List<Period> periodsList;
        protected string periodName;

        //Модальное окно
        protected Modal modal;// { get; set; }

        protected async Task CloseModal()
        {
            summ = default;
            invoice = default;
            pay = default;
            modal.ModalSize = "";

            modal.Close();
            await StateUpdate(isNotPaided);
        }

        protected void OpenModal()
        {
            modal.Open();
        }

        protected override async Task OnInitializedAsync()
        {
            await StateUpdate(isNotPaided);
            providersList = Providers;
            periodsList = Periods.OrderByDescending(p => p.ToSort()).ToList();
            providerName = providersList[0].NameProvider;
            periodName = periodsList[0].ToString();
            pay = default;
            
            invoice = default;
            isPay = default;
        }


        

        #endregion

        #region Обработка нажатия кнопок

        /// <summary>
        /// Добавить или отредактировать
        /// </summary>
        protected async Task Add()
        {
            if (!string.IsNullOrWhiteSpace(periodName) && !string.IsNullOrWhiteSpace(providerName))
            {
                provider = GetProviderByName(providerName);
                period = GetPeriodByName(periodName);
                //Добавить новый
                if (invoice == null)
                {

                    invoice = new Invoice()
                    {
                        IdPeriod = period.IdKey,
                        IdProvider = provider.IdProvider,
                        InvoiceSum = summ,
                        Pay = false
                    };
                    await Repository.AddAsync(invoice);

                }
                //Отредактировать
                else
                {
                    invoice.IdPeriod = period.IdKey;
                    invoice.IdProvider = provider.IdProvider;
                    invoice.InvoiceSum = summ;
                    invoice.Pay = pay;
                    await Repository.EditAsync(invoice);
                }

            }

            await CloseModal();
        }

        /// <summary>
        /// Изменить запись
        /// </summary>
        protected void Edit(Invoice item)
        {
            invoice = item;
            modal.Open();

            periodName = item.Period.ToString();
            providerName = item.Provider.NameProvider;
            summ = item.InvoiceSum;
            pay = item.Pay;

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

        protected void RePaid()
        {
            pay = !pay;
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

        protected Provider GetProviderByName(string name) => providersList.SingleOrDefault(n => n.NameProvider == name);

        private Period GetPeriodByName(string name) => periodsList.SingleOrDefault(n => n.ToString() == name);
    }
}
