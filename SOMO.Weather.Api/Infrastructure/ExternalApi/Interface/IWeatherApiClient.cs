using SOMO.Weather.Api.Infrastructure.ExternalApi.Models;
using System.Threading.Tasks;

namespace SOMO.Weather.Api.Infrastructure.ExternalApi.Interface
{
    public interface IWeatherApiClient
    {
        public Task<ResponseWeatherApi> Get(string city);
    }
}
