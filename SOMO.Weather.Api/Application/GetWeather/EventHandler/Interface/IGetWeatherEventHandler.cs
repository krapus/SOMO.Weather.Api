using SOMO.Weather.Api.Infrastructure.ExternalApi.Models;
using System.Threading.Tasks;

namespace SOMO.Weather.Api.Application.GetWeather.EventHandler.Interface
{
    public interface IGetWeatherEventHandler
    {
        Task Handle(ResponseWeatherApi weatherApiResponse);
    }
}
