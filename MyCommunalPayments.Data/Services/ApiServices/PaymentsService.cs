using Microsoft.AspNetCore.Components;
using MyCommunalPayments.Models.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace MyCommunalPayments.Data.Services.ApiServices
{
    public class PaymentsService : BaseService, IApiRepository<Payment>
    {
        public PaymentsService(HttpClient httpClient) : base(httpClient) { }

        #region Interface

        public async Task AddAsync(Payment item) => await httpClient.PostJsonAsync<Payment>("api/payment", item);


        public async Task EditAsync(Payment item) => await httpClient.PutJsonAsync<Payment>("api/payment", item);


        public async Task<IEnumerable<Payment>> GetAllAsync() => await httpClient.GetJsonAsync<Payment[]>("api/payment");


        public async Task<Payment> GetByIdAsync(int id) => await httpClient.GetJsonAsync<Payment>("api/payment/{id}");


        public async Task RemoveAsync(int id) => await httpClient.DeleteAsync($"api/payment/{id}");
        #endregion
    }
}
