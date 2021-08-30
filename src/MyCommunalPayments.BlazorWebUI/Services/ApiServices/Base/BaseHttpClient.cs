using System.Net.Http;

namespace MyCommunalPayments.BlazorWebUI.Services.ApiServices.Base
{
    public abstract class BaseHttpClient
    {
        protected readonly HttpClient httpClient;

        public BaseHttpClient(HttpClient httpClient) 
        {
            this.httpClient = httpClient ?? throw new System.ArgumentNullException(nameof(httpClient));
        }
    }
}
