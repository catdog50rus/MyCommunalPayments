using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using MyCommunalPayments.UI.ApiServices.Base;
using MyCommunalPayments.Models.Models;

namespace MyCommunalPayments.UI.ApiServices
{
    public class InvoiceService : BaseHttpClient, IApiRepository<Invoice>
    {
        public InvoiceService(HttpClient httpClient) : base(httpClient) { }


        #region Interface

        public async Task AddAsync(Invoice item) => await httpClient.PostJsonAsync<Invoice>("api/invoice", item);


        public async Task EditAsync(Invoice item) => await httpClient.PutJsonAsync("api/invoice", item);


        public async Task<IEnumerable<Invoice>> GetAllAsync() => await httpClient.GetJsonAsync<Invoice[]>("api/invoice");


        public async Task<Invoice> GetByIdAsync(int id) => await httpClient.GetJsonAsync<Invoice>("api/invoice/{id}");


        public async Task RemoveAsync(int id) => await httpClient.DeleteAsync($"api/invoice/{id}");

        #endregion
    }
}
