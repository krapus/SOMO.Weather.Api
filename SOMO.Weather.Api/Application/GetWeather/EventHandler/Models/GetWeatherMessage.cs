using Newtonsoft.Json;

namespace SOMO.Weather.Api.Application.GetWeather.EventHandler.Models
{
    public class GetWeatherMessage
    {
        [JsonProperty("location")]
        public string Location { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("icon")]
        public string Icon { get; set; }

        [JsonProperty("code")]
        public int Code { get; set; }
    }
}
