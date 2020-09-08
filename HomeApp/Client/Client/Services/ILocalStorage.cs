using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeApp.Client.Services
{
    public interface ILocalStorage
    {
        Task Set(string key, string value);

        ValueTask<string> Get(string key);

        Task Remove(string Key);
    }
}
