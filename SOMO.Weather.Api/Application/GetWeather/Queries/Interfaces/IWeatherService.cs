using SOMO.Weather.Api.Infrastructure.ExternalApi.Models;
using System.Threading.Tasks;

namespace SOMO.Weather.Api.Application.GetWeather.Queries.Interfaces
{
    public interface IWeatherService
    {
        Task<ResponseWeatherApi> GetCurrentWeatherByCityName(string city);
    }
}
