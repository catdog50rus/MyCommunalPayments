using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using MyCommunalPayments.BlazorWebUI.Components;
using MyCommunalPayments.Data.Services.ApiServices;
using MyCommunalPayments.Data.Services.Upload;
using MyCommunalPayments.Models.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyCommunalPayments.BlazorWebUI.Pages.Invoices
{
    public class PaymentsBase : ComponentBase
    {
        #region Поля, Инициализация формы, Модальное окно
        public PaymentViewModel PaymentViewModel { get; set; } = new PaymentViewModel();

        [Parameter]
        public Invoice Invoice { get; set; }

        [Parameter]
        public EventCallback OnPaimentReturnToInvoices { get; set; }

        [Inject] 
        public IApiRepository<Payment> Repository { get; set; }
        [Inject]
        public IApiRepository<Invoice> InvoiceRepository { get; set; }
        [Inject]
        public IFileLoad FileLoad { get; set; }
        [Inject]
        public IJSRuntime JSRuntime { get; set; }


        protected bool isUpload = default;

        protected Payment payment = default;
        protected IEnumerable<Payment> paymentsList;
        //protected int orderId;

        protected List<Invoice> invoices;
        //protected Invoice invoice;

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
                //invoice = Invoice;
                PaymentViewModel.PaymentSum = Invoice.InvoiceSum;
            } 
            await StateUpdate();
        }

        #endregion

        #region Обработка нажатия кнопок

        /// <summary>
        /// Добавить или отредактировать
        /// </summary>
        protected async Task AddAsync()
        {
            var date = PaymentViewModel.DatePayment.ToString("dd.MM.yyyy");

            if (payment == null && Invoice != null)
            {

                payment = new Payment()
                {
                    DatePayment = date,
                    IdInvoice = Invoice.IdInvoice,
                    PaymentSum = PaymentViewModel.PaymentSum,
                    Paid = PaymentViewModel.Paid,
                };

                await Repository.AddAsync(payment);

            }
            else
            {
                payment.DatePayment = date;
                payment.Paid = PaymentViewModel.Paid;
                payment.PaymentSum = PaymentViewModel.PaymentSum;
                await Repository.EditAsync(payment);
            }

            Invoice.Pay = payment.Paid;
            CloseModal();
            await InvoiceRepository.EditAsync(Invoice);
            await StateUpdate();
        }

        /// <summary>
        /// Изменить запись
        /// </summary>
        protected void Edit(Payment item)
        {
            payment = item;
            PaymentViewModel.Paid = payment.Paid;
            PaymentViewModel.DatePayment = DateTime.Parse(payment.DatePayment);
            PaymentViewModel.PaymentSum = payment.PaymentSum;
            OpenModal();
        }

        /// <summary>
        /// Удалить запись
        /// </summary>
        /// <param name="item"></param>
        protected async Task RemoveAsync(Payment item)
        {
            await Repository.RemoveAsync(item.IdPayment);
            await FileLoad.RemoveAsync(item.IdOrder);
            Invoice.Pay = false;
            await InvoiceRepository.EditAsync(Invoice);
            await StateUpdate();
            //item.Invoice.Pay = false;
        }

        protected void UploadOrder()
        {
            modal.Close();
            isUpload = true;
        }

        protected void SetOrder(int id)
        {
            if (id > 0 && payment != null)
            {
                payment.IdOrder = id;
                payment.Paid = true;
                Edit(payment);
                isUpload = false;
            }
        }

        protected async Task RemoveOrderAsync()
        {
            await FileLoad.RemoveAsync(payment.IdOrder);
            payment.IdOrder = 0;

        }

        protected async void DownloadFile(Payment item)
        {
            Order order = await FileLoad.GetOrderById(item.IdOrder);
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
            {
                paymentsList = paymentsList.Where(i => i.IdInvoice == Invoice.IdInvoice).ToList();
                if (!paymentsList.Any()) payment = default;
            }
        }
    }

    public class PaymentViewModel
    {
        [Required(ErrorMessage = "Необходимо указать дату платежа")]
        public DateTime DatePayment { get; set; } = DateTime.Today;

        /// <summary>
        /// Сумма платежа
        /// </summary>
        [Required(ErrorMessage = "Необходимо указать сумму платежа")]
        [Range(0, 1000000, ErrorMessage = "Сумма должна быть неотрицательным числом")]
        public decimal PaymentSum { get; set; }

        /// <summary>
        /// Флаг была ли произведена оплата
        /// </summary>
        public bool Paid { get; set; }
    }
}
