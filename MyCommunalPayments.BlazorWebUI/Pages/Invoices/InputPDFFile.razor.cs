using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MyCommunalPayments.Data.Services.Upload;
using System.Linq;
using System.Threading.Tasks;

namespace MyCommunalPayments.BlazorWebUI.Pages.Invoices
{
    public class InputPDFFileBase : ComponentBase
    {
        [Inject]
        public IFileLoad FileUpload { get; set; }

        protected int orderId;

        [Parameter]
        public EventCallback<int> OnUploadReturnToPaiment { get; set; }

        //IBrowserUploadFile file;

        //protected async Task HandleFileSelected(InputFileChangeEventArgs eventArgs)
        //{
        //    //file = eventArgs.pleFiles.file.File. files.FirstOrDefault();
        //    //if (file != null)
        //    //{
        //    //    await FileUpload.UploadAsync(file);
        //    //    orderId = FileUpload.OrderId;

        //    //}
        //}
    }
}
