using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Options;
using SOMO.Weather.Api.Infrastructure.EventsHandler.Interface;
using SOMO.Weather.Api.Infrastructure.Settings;
using System.Threading.Tasks;

namespace SOMO.Weather.Api.Infrastructure.EventsHandler.Implementation
{
    public class SenderMessage : ISenderMessage
    {
        private readonly ServiceBusClient _serviceBusClient;
        private readonly IOptions<AppSetting> _settings;
        public SenderMessage(IOptions<AppSetting> settings)
        {
            this._settings = settings;
            _serviceBusClient = new ServiceBusClient(_settings.Value.ServiceBusConnectionString);
        }

        async Task ISenderMessage.Send(string queueName, string data)
        {
            var sender = this._serviceBusClient.CreateSender(queueName);            
            var message = new ServiceBusMessage(data);
            await sender.SendMessageAsync(message);
        }
    }
}
