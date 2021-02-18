using System;
using System.Threading.Tasks;

namespace HomeCloud.Maps.Client.WebApi
{
    public interface IWebAPIClient
    {
        Task<T> SendAsync<T>(Func<swaggerClient, Task<T>> sendFunc);

        Task SendAsync(Func<swaggerClient, Task> sendFunc);
    }
}