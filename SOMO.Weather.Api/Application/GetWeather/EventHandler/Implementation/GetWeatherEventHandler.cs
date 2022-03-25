using Newtonsoft.Json;
using SOMO.Weather.Api.Application.GetWeather.EventHandler.Interface;
using SOMO.Weather.Api.Infrastructure.EventsHandler.Interface;
using SOMO.Weather.Api.Infrastructure.ExternalApi.Models;
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
        public async Task Handle(ResponseWeatherApi responseWeatherApi)
        {
            // Moved the queue name to another place
            // Here it is going to call our infrastructure layer to publish the event at the message broker
            await this._senderMessage.Send("rainydayeventmessages", JsonConvert.SerializeObject(responseWeatherApi));                        
        }
    }
}
