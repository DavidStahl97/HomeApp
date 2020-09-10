using HomeApp.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeApp.Client.Repository
{
    public interface IWeatherForcastRepository
    {
        Task<IEnumerable<WeatherForecast>> Get();
    }
}
