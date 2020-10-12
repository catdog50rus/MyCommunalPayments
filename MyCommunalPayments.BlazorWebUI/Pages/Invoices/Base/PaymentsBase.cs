using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using MyCommunalPayments.Data.Services.Repositories.Base;
using MyCommunalPayments.Data.Services.Upload;
using MyCommunalPayments.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyCommunalPayments.BlazorWebUI.Pages.Invoices
{
    public class PaymentsBase : ComponentBase
    {
        #region Поля, Инициализация формы, Модальное окно
        [Parameter]
        public Invoice Invoice { get; set; }

        [Parameter]
        public EventCallback OnPaimentReturnToInvoices { get; set; }

        [Inject] 
        public IRepository<Payment> Repository { get; set; }
        [Inject]
        public IFileLoad FileLoad { get; set; }
        [Inject]
        public IJSRuntime JSRuntime { get; set; }


        protected bool isUpload = default;
        protected string buttonOff = "disabled";

        protected Payment payment = default;
        protected IEnumerable<Payment> paymentsList;
        protected string datePayment;
        protected decimal paymentSum;
        protected int orderId;
        protected bool paid;

        //Модальное окно
        protected Modal modal;// { get; set; }
        protected void CloseModal()
        {
            modal.Close();
        }
        protected void OpenModal()
        {
            modal.ModalSize = "modal-lg";
            modal.Open();
        }

        protected override void OnInitialized()
        {
            paymentsList = new List<Payment>();

            paymentsList = Repository.GetAll().Where(i => i.Invoice == Invoice);

            paymentSum = Invoice.InvoiceSum;

            datePayment = DateTime.Now.Date.ToString("dd/MM/yyyy");

        }

        #endregion

        #region Обработка нажатия кнопок

        /// <summary>
        /// Добавить или отредактировать
        /// </summary>
        protected void Add()
        {
            if (buttonOff.Equals("disabled"))
            {
                return;
            }
            else
            {


                if (!string.IsNullOrWhiteSpace(datePayment) && Invoice != null && paymentSum >= 0)
                {
                    if (payment == null)
                    {
                        payment = new Payment()
                        {
                            DatePayment = datePayment,
                            Invoice = Invoice,
                            PaymentSum = paymentSum,
                            Paid = paid,
                            IdOrder = orderId

                        };
                        payment.Invoice.Pay = payment.Paid;

                        Repository.Add(payment);
                    }
                    else
                    {
                        payment.DatePayment = datePayment;
                        payment.Paid = paid;
                        payment.PaymentSum = paymentSum;
                        payment.Invoice.Pay = paid;
                        Repository.Edit(payment);
                    }
                }

                CloseModal();
            }
        }

        /// <summary>
        /// Изменить запись
        /// </summary>
        protected void Edit(Payment item)
        {
            payment = item;
            paid = item.Paid;
            datePayment = item.DatePayment;
            paymentSum = item.PaymentSum;
            OpenModal();
        }

        /// <summary>
        /// Удалить запись
        /// </summary>
        /// <param name="item"></param>
        protected void Remove(Payment item)
        {
            Repository.Remove(item);
        }

        protected void UploadOrder()
        {

            modal.Close();
            isUpload = true;
        }

        protected void SetOrder(int id)
        {
            if (id >= 0)
            {
                buttonOff = "";
                if (payment != null)
                {
                    payment.IdOrder = id;
                    Edit(payment);
                }
                else
                {
                    orderId = id;
                    OpenModal();

                }
            }
            isUpload = false;
        }

        protected async void DownloadFile(Payment payment)
        {
            Order order = FileLoad.GetOrderById(payment.IdOrder);
            if (order != null)
            {
                var content = order.OrderScreen;
                var filename = order.FileName;

                await JSRuntime.InvokeAsync<object>(
                    "FileSaveAs",
                    filename,
                    Convert.ToBase64String(content)
                );
            }

        }

        #endregion
    }
}
