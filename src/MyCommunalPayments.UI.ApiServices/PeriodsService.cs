using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using MyCommunalPayments.Models.Models;
using MyCommunalPayments.UI.ApiServices.Base;

namespace MyCommunalPayments.UI.ApiServices
{
    public class PeriodsService : BaseHttpClient, IApiRepository<Period>
    {
        public PeriodsService(HttpClient httpClient) : base(httpClient) { }

        #region Interface

        public async Task AddAsync(Period item) => await httpClient.PostJsonAsync<Period>("api/period", item);


        public async Task EditAsync(Period item)
        {
            if (item is null)
            {
                throw new ArgumentNullException(nameof(item));
            }
            await httpClient.PutJsonAsync("api/period", item);
        }


        public async Task<IEnumerable<Period>> GetAllAsync() => await httpClient.GetJsonAsync<Period[]>("api/period");


        public async Task<Period> GetByIdAsync(int id) => await httpClient.GetJsonAsync<Period>("api/period/{id}");


        public async Task RemoveAsync(int id) => await httpClient.DeleteAsync($"api/period/{id}");

        #endregion
    }
}
