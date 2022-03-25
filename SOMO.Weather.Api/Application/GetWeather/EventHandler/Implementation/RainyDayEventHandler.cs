﻿using Newtonsoft.Json;
using SOMO.Weather.Api.Application.GetWeather.EventHandler.Interface;
using SOMO.Weather.Api.Infrastructure.EventsHandler.Interface;
using SOMO.Weather.Api.Infrastructure.ExternalApi.Models;
using System.Threading.Tasks;

namespace SOMO.Weather.Api.Application.GetWeather.EventHandler.Implementation
{
    public class RainyDayEventHandler : IRainyDayEventHandler
    {
        private readonly ISenderMessage _senderMessage;
        public RainyDayEventHandler(ISenderMessage senderMessage)
        {
            this._senderMessage = senderMessage;
        }
        public async Task Handle(ResponseWeatherApi responseWeatherApi)
        {
            // Here it is going to call our infrastructure layer to publish the event at the message broker
            // Moved the queue name to another place
            await this._senderMessage.Send("weathereventmessages", JsonConvert.SerializeObject(responseWeatherApi));
        }
    }
}
