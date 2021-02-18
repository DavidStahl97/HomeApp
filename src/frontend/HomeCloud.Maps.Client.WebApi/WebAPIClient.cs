using HomeCloud.Maps.Client.WebApi.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace HomeCloud.Maps.Client.WebApi
{
    public class WebAPIClient : IWebAPIClient
    {
        private readonly swaggerClient _swaggerClient;
        private readonly HttpClient _httpClient;
        private readonly IJwtStorage _jwtStorage;

        public WebAPIClient(IJwtStorage jwtStorage, HttpClient httpClient)
        {
            _jwtStorage = jwtStorage;
            _httpClient = httpClient;
            _swaggerClient = new swaggerClient(_httpClient)
            {
                BaseUrl = _httpClient.BaseAddress.ToString()
            };
        }

        public async Task<T> SendAsync<T>(Func<swaggerClient, Task<T>> sendFunc)
        {
            await AddAuthenticationAsync();

            var result = await sendFunc(_swaggerClient);
            return result;
        }

        public async Task SendAsync(Func<swaggerClient, Task> sendFunc)
        {
            await AddAuthenticationAsync();
            await sendFunc(_swaggerClient);
        }

        private async Task AddAuthenticationAsync()
        {
            var userIdentity = await _jwtStorage.Get();
            if (userIdentity is null)
            {
                _httpClient.DefaultRequestHeaders.Authorization = null;
            }
            else
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userIdentity.IdToken);
            }
        }
    }
}
