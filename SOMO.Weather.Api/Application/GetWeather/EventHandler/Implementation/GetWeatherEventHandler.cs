using Newtonsoft.Json;
using SOMO.Weather.Api.Application.GetWeather.EventHandler.Interface;
using SOMO.Weather.Api.Application.GetWeather.EventHandler.Models;
using SOMO.Weather.Api.Infrastructure.EventsHandler.Interface;
using System.Threading.Tasks;

namespace SOMO.Weather.Api.Application.GetWeather.EventHandler.Implementation
{
    public class GetWeatherEventHandler : IGetWeatherEventHandler
    {
        private readonly ISenderMessage _senderMessage;

        public GetWeatherEventHandler(ISenderMessage senderMessage)
        {
            this._senderMessage = senderMessage;
        }
        public async Task Handle(GetWeatherMessage getWeatherMessage)
        {
            // Moved the queue name to another place
            // Here it is going to call our infrastructure layer to publish the event at the message broker
            await this._senderMessage.Send("weathereventmessages", JsonConvert.SerializeObject(getWeatherMessage));
        }
    }
}
