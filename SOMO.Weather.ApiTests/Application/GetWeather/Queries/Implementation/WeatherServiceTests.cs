using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using SOMO.Weather.Api.Application.GetWeather.EventHandler.Interface;
using SOMO.Weather.Api.Application.GetWeather.Queries.Implementation;
using SOMO.Weather.Api.Application.GetWeather.Queries.Interfaces;
using SOMO.Weather.Api.Infrastructure.ExternalApi.Interface;
using SOMO.Weather.Api.Infrastructure.ExternalApi.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SOMO.Weather.Api.Application.GetWeather.Queries.Implementation.Tests
{
    [TestClass()]
    public class WeatherServiceTests
    {
        private IWeatherApiClient? _weatherApiClient;
        private IRainyDayEventHandler? _rainyDayEventHandler;
        private IGetWeatherEventHandler? _getWeatherEventHandler;

        private IWeatherService? _weatherService;


        [TestInitialize()]
        public void Init()
        {
            //Arrange
            _weatherApiClient = Substitute.For<IWeatherApiClient>();
            _rainyDayEventHandler = Substitute.For<IRainyDayEventHandler>();
            _getWeatherEventHandler = Substitute.For<IGetWeatherEventHandler>();

            _weatherService = new WeatherService(_weatherApiClient, _rainyDayEventHandler, _getWeatherEventHandler);

        }

        [TestMethod()]
        public async Task GetCurrentWeatherByCityNameTestAsync()
        {
            // City Name = Medellin
            var cityName = "Medellin";

            // Mock Data
            _weatherApiClient.Get(cityName).ReturnsForAnyArgs(
                 new ResponseWeatherApi()
                 {
                     Current = new Current()
                     {
                         Condition = new Condition()
                         {
                             Code = 1003,
                             Icon = "//cdn.weatherapi.com/weather/64x64/day/116.png",
                             Text = "Partly cloudy"
                         }
                     },
                     IsSuccessful = true,
                     Location = new Location()
                     {
                         Name = "Medellin",
                         Region = "Antioquia",
                         Country = "Colombia",
                         Lat = 6.29,
                         Lon = -75.54,
                         LocaltimeEpoch = 0,
                         Localtime = "2022-03-28 10:36"
                     }
                 });



            var response = await _weatherService.GetCurrentWeatherByCityName("Medellin");

            // Assert
            Assert.IsTrue(response.IsSuccessful);
            Assert.AreEqual(cityName, response.Location.Name);

        }
    }
}