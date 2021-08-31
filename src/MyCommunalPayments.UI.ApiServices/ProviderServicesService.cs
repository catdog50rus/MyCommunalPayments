using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using MyCommunalPayments.Models.Models;
using MyCommunalPayments.UI.ApiServices.Base;

namespace MyCommunalPayments.UI.ApiServices
{
    public class ProviderServicesService : BaseHttpClient, IApiRepository<ProvidersServices>
    {
        public ProviderServicesService(HttpClient httpClient) : base(httpClient) { }


        #region Interface

        public async Task AddAsync(ProvidersServices item) => await httpClient.PostJsonAsync<ProvidersServices>("api/providerservices", item);


        public async Task EditAsync(ProvidersServices item) => await httpClient.PutJsonAsync<ProvidersServices>("api/providerservices", item);


        public async Task<IEnumerable<ProvidersServices>> GetAllAsync() => await httpClient.GetJsonAsync<ProvidersServices[]>("api/providerservices");


        public async Task<ProvidersServices> GetByIdAsync(int id) => await httpClient.GetJsonAsync<ProvidersServices>("api/providerservices/{id}");


        public async Task RemoveAsync(int id) => await httpClient.DeleteAsync($"api/providerservices/{id}");

        #endregion
    }
}
