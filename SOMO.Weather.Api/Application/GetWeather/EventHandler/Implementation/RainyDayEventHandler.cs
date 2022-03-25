using SOMO.Weather.Api.Application.GetWeather.EventHandler.Interface;
using System.Threading.Tasks;

namespace SOMO.Weather.Api.Application.GetWeather.EventHandler.Implementation
{
    public class RainyDayEventHandler : IRainyDayEventHandler
    {
        public Task<bool> Handle()
        {
            // Here it is going to call our infrastructure layer to publish the event at the message broker
            return Task.FromResult(true);
        }
    }
}
