using HomeCloud.Client.Services;
using HomeCloud.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeCloud.Client.Repository
{
    public class WeatherForacastRepository : IWeatherForcastRepository
    {
        private const string BASE_URL = "api/WeatherForecast";
        private readonly IHttpService _httpService;

        public WeatherForacastRepository(IHttpService httpService)
        {
            _httpService = httpService;
        }

        public async Task<IEnumerable<WeatherForecast>> GetAllAsync()
        {
            var response = await _httpService.Get<IEnumerable<WeatherForecast>>(BASE_URL);

            if (response.Success)
            {
                return response.Response;
            }

            throw new ApplicationException(await response.GetBody());
        }
    }
}
