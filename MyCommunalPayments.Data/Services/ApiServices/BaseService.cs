using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace MyCommunalPayments.Data.Services.ApiServices
{
    public abstract class BaseService
    {
        protected readonly HttpClient httpClient;

        public BaseService(HttpClient httpClient) => this.httpClient = httpClient;
    }
}
