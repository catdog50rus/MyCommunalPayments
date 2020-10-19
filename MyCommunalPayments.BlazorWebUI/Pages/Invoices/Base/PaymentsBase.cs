using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using MyCommunalPayments.Data.Services.ApiServices;
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
        public IApiRepository<Payment> Repository { get; set; }
        [Inject]
        public IFileLoad FileLoad { get; set; }
        [Inject]
        public IJSRuntime JSRuntime { get; set; }


        protected bool isUpload = default;

        protected Payment payment = default;
        protected IEnumerable<Payment> paymentsList;
        protected string datePayment;
        protected decimal paymentSum;
        protected int orderId;
        protected bool paid;

        protected List<Invoice> invoices;
        protected Invoice invoice;

        //Модальное окно
        protected Modal modal = new Modal();
        protected void CloseModal()
        {
            modal.Close();
        }
        protected void OpenModal()
        {
            modal.ModalSize = "modal-lg";
            modal.Open();
        }

        protected override async Task OnInitializedAsync()
        {
            if (Invoice != null) 
            {
                invoice = Invoice;
                paymentSum = invoice.InvoiceSum;
            } 
            await StateUpdate();
            
            datePayment = DateTime.Now.Date.ToString("dd/MM/yyyy");
        }

        #endregion

        #region Обработка нажатия кнопок

        /// <summary>
        /// Добавить или отредактировать
        /// </summary>
        protected async Task Add()
        {

            if (!string.IsNullOrWhiteSpace(datePayment) && paymentSum >= 0)
            {
                if (payment == null && Invoice != null)
                {
                    
                    payment = new Payment()
                    {
                        DatePayment = datePayment,
                        IdInvoice = invoice.IdInvoice,
                        PaymentSum = paymentSum,
                        Paid = paid,
                        IdOrder = orderId

                    };
                    Invoice.Pay = payment.Paid;

                    await Repository.AddAsync(payment);
                }
                else
                {
                    payment.DatePayment = datePayment;
                    payment.Paid = paid;
                    payment.PaymentSum = paymentSum;
                    Invoice.Pay = paid;
                    payment.IdOrder = orderId;
                    await Repository.EditAsync(payment);
                }
            }

            CloseModal();
            await StateUpdate();
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
            invoice = item.Invoice;
            orderId = item.IdOrder;
            OpenModal();
        }

        /// <summary>
        /// Удалить запись
        /// </summary>
        /// <param name="item"></param>
        protected async Task Remove(Payment item)
        {
            await Repository.RemoveAsync(item.IdPayment);
            await StateUpdate();
            item.Invoice.Pay = false;
        }

        protected void UploadOrder()
        {

            modal.Close();
            isUpload = true;
        }

        protected void SetOrder(int id)
        {
            if (id > 0)
            {
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
            Order order = await FileLoad.GetOrderById(payment.IdOrder);
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

        private async Task StateUpdate()
        {
            paymentsList = await Repository.GetAllAsync();
            paymentsList = paymentsList.OrderByDescending(d => d.Invoice.Period.ToSort());
            if (Invoice != null)
                paymentsList = paymentsList.Where(i => i.IdInvoice == invoice.IdInvoice);
        }
    }
}
