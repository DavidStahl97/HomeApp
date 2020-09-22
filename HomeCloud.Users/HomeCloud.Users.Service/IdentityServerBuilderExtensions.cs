using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace HomeCloud.Users.Service
{
    public static class IdentityServerBuilderExtensions
    {
        public static IIdentityServerBuilder LoadSigningCredentialFrom(this IIdentityServerBuilder builder, string path)
        {            
            if (string.IsNullOrEmpty(path) == false)
            {
                var certificate = new X509Certificate2(path, "test123");
                return builder.AddSigningCredential(certificate);
            }
            else
            {
                return builder.AddDeveloperSigningCredential();
            }
        }
    }
}
