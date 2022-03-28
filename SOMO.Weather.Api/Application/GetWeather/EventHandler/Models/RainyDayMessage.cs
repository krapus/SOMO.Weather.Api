using Newtonsoft.Json;

namespace SOMO.Weather.Api.Application.GetWeather.EventHandler.Models
{
    public class RainyDayMessage
    {

        [JsonProperty("location")]
        public string Location { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("code")]
        public string Code { get; set; }
    }
}
