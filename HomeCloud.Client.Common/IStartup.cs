using HomeCloud.Client.Common.Model;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace HomeCloud.Client.Common
{
    public interface IStartup
    {
        App App { get; }

        void ConfigureServices(IServiceCollection services);
    }
}
