using Newtonsoft.Json;

namespace AdamSmithBot.Models
{
    public class NbuResponse
    {
        [JsonProperty("rate")]
        public double Rate { get; set; }

        [JsonProperty("exchangedate")]
        public string ExchangeDate { get; set; }
    }
}
