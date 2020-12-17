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

        /// <summary>
        /// Объявление и инициализация модели представления
        /// </summary>
        protected PaymentViewModel paymentViewModel = new PaymentViewModel();

        /// <summary>
        /// Получаемая квитанция
        /// </summary>
        [Parameter]
        public Invoice Invoice { get; set; }

        [Parameter]
        public EventCallback OnPaimentReturnToInvoices { get; set; }

        //Зависимости
        [Inject] 
        public IApiRepository<Payment> Repository { get; set; }
        [Inject]
        public IApiRepository<Invoice> InvoiceRepository { get; set; }
        [Inject]
        public IFileLoad FileLoad { get; set; }
        [Inject]
        public IJSRuntime JSRuntime { get; set; }

        /// <summary>
        /// Переменная для переключения интерфейса (платежи/загрузка файла) 
        /// </summary>
        protected bool isUpload = default;

        /// <summary>
        /// Платеж
        /// </summary>
        protected Payment payment = default;
        /// <summary>
        /// Список платежей
        /// </summary>
        protected IEnumerable<Payment> paymentsList;

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
                paymentViewModel.PaymentSum = Invoice.InvoiceSum;
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
            //Получаем дату платежа в текстовом виде, как предусматривает модель
            var date = paymentViewModel.DatePayment.ToString("dd.MM.yyyy");

            //Проверяем есть ли активная модель
            if (payment == null && Invoice != null)
            {
                //Создаем экземпляр модели
                payment = new Payment()
                {
                    DatePayment = date,
                    IdInvoice = Invoice.IdInvoice,
                    PaymentSum = paymentViewModel.PaymentSum,
                    Paid = paymentViewModel.Paid,
                };
                //Добавляем модель в БД
                await Repository.AddAsync(payment);

            }
            else
            {
                //Обновляем модель и записываем изменения в БД
                payment.DatePayment = date;
                payment.Paid = paymentViewModel.Paid;
                payment.PaymentSum = paymentViewModel.PaymentSum;
                await Repository.EditAsync(payment);
            }
            //Меняем поле оплаты в квитанции и записываем в БД
            Invoice.Pay = payment.Paid;
            await InvoiceRepository.EditAsync(Invoice);
            CloseModal();
            await StateUpdate();
        }

        /// <summary>
        /// Изменить запись
        /// </summary>
        protected void Edit(Payment item)
        {
            payment = item;
            paymentViewModel.Paid = payment.Paid;
            paymentViewModel.DatePayment = DateTime.Parse(payment.DatePayment);
            paymentViewModel.PaymentSum = payment.PaymentSum;
            OpenModal();
        }

        /// <summary>
        /// Удалить запись
        /// </summary>
        /// <param name="item"></param>
        protected async Task RemoveAsync(Payment item)
        {
            //Удаляем платеж и вносим изменения в БД
            await Repository.RemoveAsync(item.IdPayment);
            await FileLoad.RemoveAsync(item.IdOrder);
            Invoice.Pay = false;
            await InvoiceRepository.EditAsync(Invoice);
            await StateUpdate();
        }

        /// <summary>
        /// Загрузка файла в БД
        /// </summary>
        protected void UploadOrder()
        {
            //Закрываем модальное окно и переходим к загрузке файла
            modal.Close();
            isUpload = true;
        }

        /// <summary>
        /// Обработка данных о загруженной файле
        /// </summary>
        protected void SetOrder(int id)
        {
            //Если файл загружен, проверяем его id
            //Если id и платежка существуют, вносим изменения в модели и БД
            if (id > 0 && payment != null)
            {
                payment.IdOrder = id;
                payment.Paid = true;
                Edit(payment);
                
            }
            else
            {
                OpenModal();
            }
            isUpload = false;
        }

        /// <summary>
        /// Удаление файла платежки из БД
        /// </summary>
        /// <returns></returns>
        protected async Task RemoveOrderAsync()
        {
            //Проверяем модель а наличие записи о файле платежки и удаляем ее
            if (payment.IdOrder != 0)
            {
                await FileLoad.RemoveAsync(payment.IdOrder);
                payment.IdOrder = 0;
            }

        }

        /// <summary>
        /// Скачать файл платежки из БД
        /// </summary>
        /// <param name="item"></param>
        protected async void DownloadFile(Payment item)
        {
            //Проверяем есть ли в БД файл платежки
            Order order = await FileLoad.GetOrderById(item.IdOrder);
            if (order != null)
            {
                //Получаем из БД файл в виде массива байтов и имя файла
                var content = order.OrderScreen;
                var filename = order.FileName;

                //Сохраняем файл на диск
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

    /// <summary>
    /// Модель представления 
    /// </summary>
    public class PaymentViewModel
    {
        /// <summary>
        /// Дата платежа
        /// </summary>
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
