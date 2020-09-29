using Newtonsoft.Json;

namespace AdamSmithBot.Models
{
    public class TelegramMessage
    {
        [JsonProperty("chat")]
        public TelegramChat Chat { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }
    }
}
