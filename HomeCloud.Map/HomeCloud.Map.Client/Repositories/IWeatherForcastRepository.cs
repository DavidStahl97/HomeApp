using HomeCloud.Map.Shared;
using HomeCloud.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeCloud.Map.Client.Repository
{
    public interface IWeatherForcastRepository
    {
        Task<IEnumerable<WeatherForecast>> GetAllAsync();
    }
}
