using HomeCloud.Maps.Application.Database.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeCloud.Maps.Application.Database
{
    public interface IRepository
    {
        IUserSettingsCollection UserSettingsCollection { get; }
    }
}
