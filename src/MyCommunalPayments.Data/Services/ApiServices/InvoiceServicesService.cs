using Microsoft.AspNetCore.Components;
using MyCommunalPayments.Models.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace MyCommunalPayments.Data.Services.ApiServices
{
    public class InvoiceServicesService : BaseService, IApiRepository<InvoiceServices>
    {
        public InvoiceServicesService(HttpClient httpClient) : base(httpClient) { }


        #region Interface

        public async Task AddAsync(InvoiceServices item) => await httpClient.PostJsonAsync<InvoiceServices>("api/invoiceservices", item);


        public async Task EditAsync(InvoiceServices item) => await httpClient.PutJsonAsync<InvoiceServices>("api/invoiceservices", item);


        public async Task<IEnumerable<InvoiceServices>> GetAllAsync() => await httpClient.GetJsonAsync<InvoiceServices[]>("api/invoiceservices");


        public async Task<InvoiceServices> GetByIdAsync(int id) => await httpClient.GetJsonAsync<InvoiceServices>("api/invoiceservices/{id}");


        public async Task RemoveAsync(int id) => await httpClient.DeleteAsync($"api/invoiceservices/{id}");

        #endregion
    }
}
