using HomeCloud.Client.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeCloud.Client.Authentication
{
    public class JwtStorage : IJwtStorage
    {
        private readonly IStorage _storage;

        public JwtStorage(IStorage storage)
        {
            _storage = storage;
        }

        public async Task<UserIdentity> Get()
        {
            string key = "Microsoft.AspNetCore.Components.WebAssembly.Authentication.CachedAuthSettings";
            var authSettings = await _storage.Get<AuthenticationSettings>(key);
            if (authSettings is null)
            {
                return null;
            }

            var jwt = await _storage.Get<UserIdentity>(authSettings?.OIDCUserKey);
            return jwt;
        }

    }
}
