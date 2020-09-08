using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeApp.Client.Services
{
    public class LocalStorage : ILocalStorage
    {
        private readonly IJSRuntime _jSRuntime;

        public LocalStorage(IJSRuntime jSRuntime)
        {
            _jSRuntime = jSRuntime;
        }

        public ValueTask<string> Get(string key)
        {
            return _jSRuntime.InvokeAsync<string>("localStorage.getItem", key);
        }

        public async Task Remove(string key)
        {
            await _jSRuntime.InvokeAsync<object>("localStorage.removeItem", key);
        }

        public async Task Set(string key, string value)
        {
            await _jSRuntime.InvokeAsync<object>("localStorage.setItem", key, value);
        }
    }
}
