using System.Threading.Tasks;

namespace SOMO.Weather.Api.Application.GetWeather.EventHandler.Interface
{
    public interface IRainyDayEventHandler
    {
        Task<bool> Handle();
    }
}
