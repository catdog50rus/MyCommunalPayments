using Microsoft.AspNetCore.Components;
using MyCommunalPayments.Models.Models;

namespace MyCommunalPayments.BlazorWebUI.Components
{
    public class RowServiceCountersTableBase : ComponentBase
    {
        [Parameter] public ServiceCounter ServicesCounter { get; set; }
        [Parameter] public EventCallback<ServiceCounter> RemoveEvent { get; set; }
        [Parameter] public EventCallback<ServiceCounter> EditEvent { get; set; }
    }
}
