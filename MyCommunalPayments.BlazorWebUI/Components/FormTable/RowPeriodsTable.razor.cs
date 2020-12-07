using Microsoft.AspNetCore.Components;
using MyCommunalPayments.Models.Models;

namespace MyCommunalPayments.BlazorWebUI.Components
{
    public class RowPeriodsTableBase : ComponentBase
    {
        [Parameter] public Period Period { get; set; }
        [Parameter] public EventCallback<Period> RemoveEvent { get; set; }
        [Parameter] public EventCallback<Period> EditEvent { get; set; }
    }
}
