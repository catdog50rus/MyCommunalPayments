using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using MyCommunalPayments.Models.Models;
using MyCommunalPayments.UI.ApiServices.Base;

namespace MyCommunalPayments.UI.ApiServices
{
    public class ServiceCounterService : BaseHttpClient, IApiRepository<ServiceCounter>
    {
        public ServiceCounterService(HttpClient httpClient) : base(httpClient) { }

        #region Interface

        public async Task AddAsync(ServiceCounter item) => await httpClient.PostJsonAsync<ServiceCounter>("api/servicecounter", item);


        public async Task EditAsync(ServiceCounter item) => await httpClient.PutJsonAsync<ServiceCounter>("api/servicecounter", item);


        public async Task<IEnumerable<ServiceCounter>> GetAllAsync() => await httpClient.GetJsonAsync<ServiceCounter[]>("api/servicecounter");


        public async Task<ServiceCounter> GetByIdAsync(int id) => await httpClient.GetJsonAsync<ServiceCounter>("api/servicecounter/{id}");


        public async Task RemoveAsync(int id) => await httpClient.DeleteAsync($"api/servicecounter/{id}");

        #endregion
    }
}
