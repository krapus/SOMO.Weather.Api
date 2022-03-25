using SOMO.Weather.Api.Application.GetWeather.EventHandler.Interface;
using SOMO.Weather.Api.Application.GetWeather.Queries.Interfaces;
using SOMO.Weather.Api.Infrastructure.ExternalApi.Interface;
using SOMO.Weather.Api.Infrastructure.ExternalApi.Models;
using System.Threading.Tasks;

namespace SOMO.Weather.Api.Application.GetWeather.Queries.Implementation
{
    public class WeatherService : IWeatherService
    {
        private readonly IWeatherApiClient _weatherApiClient;
        private readonly IRainyDayEventHandler _rainyDayEventHandler;
        private readonly IGetWeatherEventHandler _getWeatherEventHandler;

        public WeatherService(IWeatherApiClient weatherApiClient, IRainyDayEventHandler rainyDayEventHandler, IGetWeatherEventHandler getWeatherEventHandler)
        {
            this._weatherApiClient = weatherApiClient;
            this._rainyDayEventHandler = rainyDayEventHandler;
            this._getWeatherEventHandler = getWeatherEventHandler;
        }
        public async Task<ResponseWeatherApi> GetCurrentWeatherByCityName(string city)
        {
            var weatherApiResponse = await this._weatherApiClient.Get(city);

            // Save the data in Azure Storage
            // Call our infrastructure to save the data as Event
            await this._getWeatherEventHandler.Handle(weatherApiResponse);


            // This code should be out of the code
            if (weatherApiResponse.Current.Condition.Code == 1183)
            {
                // Here call event hadler
                // Probably this need to be review it the **await process**, we can handle in a direrente way
                // it depends if we want to wait for an answer or what about if any error happend
                await this._rainyDayEventHandler.Handle(weatherApiResponse);
            }

            return weatherApiResponse;
        }
    }
}
