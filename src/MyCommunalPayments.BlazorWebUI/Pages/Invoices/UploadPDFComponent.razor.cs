using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using MyCommunalPayments.UI.ApiServices.Interfaces;

namespace MyCommunalPayments.BlazorWebUI.Pages.Invoices
{
    public class UploadPDFComponentBase : ComponentBase
    {
        protected EditContext editContext;
        protected UploadViewModel uploadViewModel;

        [Inject]
        public IFileService FileService { get; set; }

        [Parameter]
        public EventCallback<int> OnUploadReturnToPayment { get; set; }

        protected int orderId;

        //Элементы управления интерфейсом
        protected bool isSubmit;
        protected bool isLoad;
        protected bool isDisable;

        protected override void OnInitialized()
        {
            uploadViewModel = new UploadViewModel();
            editContext = new EditContext(uploadViewModel);
        }

        /// <summary>
        /// Обработка выбора файла в браузере
        /// </summary>
        /// <param name="eventArgs"></param>
        protected void OnChange(InputFileChangeEventArgs eventArgs)
        {
            //Получаем файл из браузера
            uploadViewModel.File = eventArgs.File;
            editContext.NotifyFieldChanged(FieldIdentifier.Create(() => uploadViewModel.File));
            //Если тип файла совпадает с *.pdf показываем кнопку подтвердить
            isSubmit = Path.GetExtension(uploadViewModel.File.Name).Equals(".pdf");
        }

        /// <summary>
        /// Подтверждаем выбор
        /// </summary>
        protected void OnSubmit()
        {
            //Меняем интерфейс
            isSubmit = false;
            isLoad = true;
            isDisable = false;
        }

        /// <summary>
        /// Показать / Скрыть уведомление 
        /// </summary>
        protected void Disabled()
        {
            isDisable = !isDisable;
        }

        /// <summary>
        /// Запись файла в БД
        /// </summary>
        /// <returns></returns>
        protected async Task SaveFileAsync()
        {
            isLoad = false;

            var file = uploadViewModel.File;

            //Получаем id
            orderId = await FileService.UploadFile(file);
            isLoad = false;
        }

        /// <summary>
        /// Модель представления с кастомной валидацией
        /// </summary>
        public class UploadViewModel
        {
            [Required(ErrorMessage = "Необходимо выбрать файл со сканом платежки")]
            [FileValidation(new[] { ".pdf" })]
            public IBrowserFile File { get; set; }
        }

        /// <summary>
        /// Реализация валидации модели на тип файла
        /// </summary>
        private class FileValidationAttribute : ValidationAttribute
        {
            public FileValidationAttribute(string[] allowedExtensions)
            {
                AllowedExtensions = allowedExtensions;
            }

            private string[] AllowedExtensions { get; }

            protected override ValidationResult IsValid(object value, ValidationContext validationContext)
            {
                var file = (IBrowserFile)value;

                var extension = Path.GetExtension(file.Name);

                if (!AllowedExtensions.Contains(extension, StringComparer.OrdinalIgnoreCase))
                {
                    return new ValidationResult($"Файл должен иметь расширение: {string.Join(", ", AllowedExtensions)}.", new[] { validationContext.MemberName });
                }

                return ValidationResult.Success;
            }
        }
    }
}
