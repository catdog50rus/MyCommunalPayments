using Microsoft.AspNetCore.Components;
using MyCommunalPayments.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyCommunalPayments.BlazorWebUI.Components
{
    public class RowServicesTableBase : ComponentBase
    {
        [Parameter] public Service Service { get; set; }
        [Parameter] public EventCallback<Service> RemoveEvent { get; set; }
        [Parameter] public EventCallback<Service> EditEvent { get; set; }
    }
}
