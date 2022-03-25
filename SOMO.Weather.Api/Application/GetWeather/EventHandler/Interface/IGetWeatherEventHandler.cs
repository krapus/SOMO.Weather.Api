using SOMO.Weather.Api.Application.GetWeather.EventHandler.Models;
using SOMO.Weather.Api.Infrastructure.ExternalApi.Models;
using System.Threading.Tasks;

namespace SOMO.Weather.Api.Application.GetWeather.EventHandler.Interface
{
    public interface IGetWeatherEventHandler
    {
        Task Handle(GetWeatherMessage getWeatherMessage);
    }
}
