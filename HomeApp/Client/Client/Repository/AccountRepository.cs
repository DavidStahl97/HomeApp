using HomeApp.Client.Services;
using HomeApp.Shared.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeApp.Client.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private const string BASE_URL = "api/account";
        private readonly IHttpService _httpService;

        public AccountRepository(IHttpService httpService)
        {
            _httpService = httpService;
        }

        public async Task<UserToken> Register(UserInfo userInfo)
        {
            var response = await _httpService.Post<UserInfo, UserToken>($"{BASE_URL}/create", userInfo);
            if (!response.Success)
            {
                throw new ApplicationException(await response.GetBody());
            }

            return response.Response;
        }
    }
}
