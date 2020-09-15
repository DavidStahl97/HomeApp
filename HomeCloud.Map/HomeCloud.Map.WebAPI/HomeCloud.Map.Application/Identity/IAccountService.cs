using HomeCloud.Shared.DataTransferObjects;
using OneOf;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HomeCloud.Application.Identity
{
    public interface IAccountService
    {
        Task<OneOf<UserToken, InvalidUserCreation>> CreateUser(UserInfo userInfo);

        Task<OneOf<UserToken, InvalidLogin>> Login(UserInfo userInfo);
    }
}
