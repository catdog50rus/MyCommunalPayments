using System.Net.Http;

namespace MyCommunalPayments.UI.ApiServices.Base
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
