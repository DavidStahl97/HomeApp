using HomeApp.Shared.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeApp.Client.Repository
{
    public interface IAccountRepository
    {
        Task<UserToken> Register(UserInfo userInfo);
    }
}
