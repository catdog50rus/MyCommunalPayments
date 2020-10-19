using Microsoft.AspNetCore.Components;
using MyCommunalPayments.Models.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace MyCommunalPayments.Data.Services.ApiServices
{
    public class ServicesService : BaseService, IApiRepository<Service>
    {
        public ServicesService(HttpClient httpClient) : base(httpClient) { }

        #region Interface

        public async Task AddAsync(Service item) => await httpClient.PostJsonAsync<Service>("api/service", item);
        

        public async Task EditAsync(Service item) => await httpClient.PutJsonAsync<Service>("api/service", item);
        

        public async Task<IEnumerable<Service>> GetAllAsync() => await httpClient.GetJsonAsync<Service[]>("api/service");

        
        public async Task<Service> GetByIdAsync(int id) => await httpClient.GetJsonAsync<Service>("api/service/{id}");


        public async Task RemoveAsync(int id) => await httpClient.DeleteAsync($"api/service/{id}");

        #endregion
    }
}
