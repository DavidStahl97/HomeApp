using HomeCloud.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeCloud.Client.Repository
{
    public interface IWeatherForcastRepository
    {
        Task<IEnumerable<WeatherForecast>> GetAllAsync();
    }
}
