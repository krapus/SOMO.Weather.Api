using SOMO.Weather.Api.Application.GetWeather.EventHandler.Interface;
using SOMO.Weather.Api.Application.GetWeather.Queries.Interfaces;
using SOMO.Weather.Api.Infrastructure.ExternalApi.Interface;
using SOMO.Weather.Api.Infrastructure.ExternalApi.Models;
using System.Collections.Generic;
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
            try
            {
                var weatherApiResponse = await this._weatherApiClient.Get(city);

                // Save the data in Azure Storage
                // Call our infrastructure to save the data as Event
                await this._getWeatherEventHandler.Handle(new EventHandler.Models.GetWeatherMessage()
                {
                    Code = weatherApiResponse.Current.Condition.Code,
                    Icon = weatherApiResponse.Current.Condition.Icon,
                    Text = weatherApiResponse.Current.Condition.Text
                });

                // Possibles codes for rainy days
                var rainyCodes = new List<int>()
                {
                    1063,
                    1003,
                    1189,
                    1180
                };

                // This condition code should be out of the code
                if (rainyCodes.Contains(weatherApiResponse.Current.Condition.Code))
                {
                    // Here call event hadler
                    // Probably this need to be review it the **await process**, we can handle in a direrente way
                    // it depends if we want to wait for an answer or what about if any error happend
                    await this._rainyDayEventHandler.Handle(new EventHandler.Models.RainyDayMessage()
                    { 
                        Code = "( ͡°ʖ̯ ͡°)", 
                        Message = "It doesn't seem like a nice day to be outside, make coffee and keep coding" }
                    );
                }

                weatherApiResponse.IsSuccessful = true;
                return weatherApiResponse;
            }
            catch (System.Exception)
            {
                return new ResponseWeatherApi()
                {
                    IsSuccessful = false
                };
            }
        }
    }
}
