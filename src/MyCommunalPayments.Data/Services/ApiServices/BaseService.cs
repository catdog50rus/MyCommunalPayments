using System.Net.Http;

namespace MyCommunalPayments.Data.Services.ApiServices
{
    public abstract class BaseService
    {
        protected readonly HttpClient httpClient;

        public BaseService(HttpClient httpClient) 
        {
            this.httpClient = httpClient ?? throw new System.ArgumentNullException(nameof(httpClient));
        }
    }
}
