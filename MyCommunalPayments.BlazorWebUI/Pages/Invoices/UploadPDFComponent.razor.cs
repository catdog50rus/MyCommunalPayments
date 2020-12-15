using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MyCommunalPayments.Data.Services.Upload;
using MyCommunalPayments.Models.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MyCommunalPayments.BlazorWebUI.Pages.Invoices
{
    public class UploadPDFComponentBase : ComponentBase, IDisposable
    {
        private CancellationTokenSource cancelation;
        protected EditContext editContext;
        protected UploadViewModel uploadViewModel;

        [Inject]
        public IFileLoad FileUploadRepository { get; set; }

        [Parameter]
        public EventCallback<int> OnUploadReturnToPayment { get; set; }

        protected int orderId;

        protected bool isSubmit;
        protected bool isLoad;
        protected bool isDisable;

        protected override void OnInitialized()
        {
            cancelation = new CancellationTokenSource();
            uploadViewModel = new UploadViewModel();
            editContext = new EditContext(uploadViewModel);
        }

        protected void OnChange(InputFileChangeEventArgs eventArgs)
        {
            uploadViewModel.File = eventArgs.File;
            editContext.NotifyFieldChanged(FieldIdentifier.Create(() => uploadViewModel.File));

            isSubmit = Path.GetExtension(uploadViewModel.File.Name).Equals(".pdf");
            isDisable = false;
        }

        protected void OnSubmit()
        {
            isSubmit = false;
            isLoad = true;
            isDisable = false;
        }

        protected void Disabled()
        {
            isDisable = !isDisable;
        }

        protected async Task SaveFileAsync()
        {
            using var stream = uploadViewModel.File.OpenReadStream();

            var ms = new MemoryStream();
            Guid guid = Guid.NewGuid();
            var filename = $"{guid}.pdf";

            await stream.CopyToAsync(ms);
            var order = new Order()
            {
                OrderScreen = ms.ToArray(),
                FileName = filename
            };

            await FileUploadRepository.UploadAsync(order);
            orderId = FileUploadRepository.OrderId;
            isLoad = false;
        }

        public void Dispose() => cancelation.Cancel();

        public class UploadViewModel
        {
            [Required(ErrorMessage = "Необходимо выбрать файл со скааном платежки")]
            [FileValidation(new[] { ".pdf" })]
            public IBrowserFile File { get; set; }
        }

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

                var extension = System.IO.Path.GetExtension(file.Name);

                if (!AllowedExtensions.Contains(extension, StringComparer.OrdinalIgnoreCase))
                {
                    return new ValidationResult($"Файл должен иметь расширение: {string.Join(", ", AllowedExtensions)}.", new[] { validationContext.MemberName });
                }

                return ValidationResult.Success;
            }
        }
    }
}
