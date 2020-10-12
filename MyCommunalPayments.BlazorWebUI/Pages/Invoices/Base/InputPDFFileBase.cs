using BlazorInputFile;
using Microsoft.AspNetCore.Components;
using MyCommunalPayments.Data.Services.Upload;
using System.Linq;
using System.Threading.Tasks;

namespace MyCommunalPayments.BlazorWebUI.Pages.Invoices
{
    public class InputPDFFileBase : ComponentBase
    {
        [Inject]
        public IFileLoad FileUpload { get; set; }

        protected int orderId = -1;

        [Parameter]
        public EventCallback<int> OnUploadReturnToPaiment { get; set; }

        IFileListEntry file;

        protected async Task HandleFileSelected(IFileListEntry[] files)
        {
            file = files.FirstOrDefault();
            if (file != null)
            {
                await FileUpload.UploadAsync(file);
                orderId = FileUpload.OrderId;

            }
        }
    }
}
