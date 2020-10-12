using Microsoft.AspNetCore.Components;
using MyCommunalPayments.Data.Services.Repositories.Base;
using MyCommunalPayments.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyCommunalPayments.BlazorWebUI.Pages.Invoices
{
    public class InvoicesBase : ComponentBase
    {
        #region Поля, Инициализация формы, Модальное окно
        [Parameter]
        public IRepository<Invoice> Repository { get; set; }

        [Parameter]
        public List<Provider> Providers { get; set; }

        [Parameter]
        public List<Period> Periods { get; set; }

        [Parameter]
        public List<Service> Services { get; set; }

        [Parameter]
        public EventCallback<Invoice> OnClickSetService { get; set; }

        protected IEnumerable<Invoice> invoices;
        protected Invoice invoice;
        protected decimal summ;
        protected bool pay;
        protected bool isNotPaided;
        protected bool isPay;

        //Providers
        protected Provider provider;
        protected List<Provider> providersList;
        protected string providerName;

        //Periods
        protected Period period;
        protected List<Period> periodsList;
        protected string periodName;

        //Services
        protected List<Service> servicesList;


        //Модальное окно
        protected Modal modal;// { get; set; }

        protected void CloseModal()
        {
            summ = default;
            invoice = default;
            pay = default;
            modal.ModalSize = "";

            modal.Close();
        }

        protected void OpenModal()
        {
            modal.Open();
        }

        protected override void OnInitialized()
        {
            providersList = Providers;
            periodsList = Periods.OrderByDescending(p => p.ToSort()).ToList();
            servicesList = Services;
            ShowPaided();
            providerName = providersList[0].NameProvider;
            periodName = periodsList[0].ToString();
            pay = default;
            isNotPaided = true;
            invoice = default;
            isPay = default;
        }

        protected Provider GetProviderByName(string name) => providersList.SingleOrDefault(n => n.NameProvider == name);

        protected Period GetPeriodByName(string name) => periodsList.SingleOrDefault(n => n.ToString() == name);

        #endregion

        #region Обработка нажатия кнопок

        /// <summary>
        /// Добавить или отредактировать
        /// </summary>
        protected void Add()
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
                        Period = period,
                        Provider = provider,
                        InvoiceSum = summ,
                        Pay = false
                    };
                    Repository.Add(invoice);

                }
                //Отредактировать
                else
                {
                    invoice.Period = period;
                    invoice.Provider = provider;
                    invoice.InvoiceSum = summ;
                    invoice.Pay = pay;
                    Repository.Edit(invoice);
                }

            }

            CloseModal();
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
        protected void Remove(Invoice item)
        {
            Repository.Remove(item);
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

        protected void ShowPaided()
        {
            isNotPaided = !isNotPaided;
            if (isNotPaided)
            {
                invoices = Repository.GetAll().Where(i => i.Pay == false).OrderByDescending(p => p.Period.ToSort());
            }
            else
            {
                invoices = Repository.GetAll().OrderByDescending(p => p.Period.ToSort());
            }
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
    }
}
