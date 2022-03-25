using Microsoft.Extensions.Options;
using RestSharp;
using SOMO.Weather.Api.Infrastructure.ExternalApi.Interface;
using SOMO.Weather.Api.Infrastructure.ExternalApi.Models;
using SOMO.Weather.Api.Infrastructure.Settings;
using System.Threading.Tasks;

namespace SOMO.Weather.Api.Infrastructure.ExternalApi.Implementation
{
    public class WeatherApiClient : IWeatherApiClient
    {
        private readonly RestClient _restClient;
        private readonly IOptions<AppSetting> _settings;
        public WeatherApiClient(IOptions<AppSetting> settings)
        {
            this._settings = settings;
            this._restClient = new RestClient(_settings.Value.WeatherApiUrl);
        }
        public async Task<ResponseWeatherApi> Get(string city)
        {
            var request = new RestRequest(string.Empty, Method.Get)
                .AddParameter("key", _settings.Value.ApiKey)
                .AddParameter("q", city)
                .AddParameter("aqi", "no");

            return await this._restClient.GetAsync<ResponseWeatherApi>(request);
        }
    }
}
