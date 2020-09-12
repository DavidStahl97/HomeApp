using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeApp.Client.Authentication
{
    public interface IJwtStorage
    {
        Task<UserIdentity> Get();
    }
}
