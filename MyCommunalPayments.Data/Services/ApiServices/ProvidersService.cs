using Microsoft.AspNetCore.Components;
using MyCommunalPayments.Models.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace MyCommunalPayments.Data.Services.ApiServices
{
    public class ProvidersService : BaseService, IApiRepository<Provider>
    {
        public ProvidersService(HttpClient httpClient) : base(httpClient) { }

        #region Interface

        public async Task AddAsync(Provider item) => await httpClient.PostJsonAsync<Provider>("api/provider", item);


        public async Task EditAsync(Provider item) => await httpClient.PutJsonAsync<Provider>("api/provider", item);


        public async Task<IEnumerable<Provider>> GetAllAsync() => await httpClient.GetJsonAsync<Provider[]>("api/provider");


        public async Task<Provider> GetByIdAsync(int id) => await httpClient.GetJsonAsync<Provider>("api/provider/{id}");


        public async Task RemoveAsync(int id) => await httpClient.DeleteAsync($"api/provider/{id}");
        #endregion


    }
}
