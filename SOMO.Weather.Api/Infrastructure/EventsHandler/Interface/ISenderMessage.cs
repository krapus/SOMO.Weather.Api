using System.Threading.Tasks;

namespace SOMO.Weather.Api.Infrastructure.EventsHandler.Interface
{
    public interface ISenderMessage
    {
        public Task Send(string queueName, string message);
    }
}
