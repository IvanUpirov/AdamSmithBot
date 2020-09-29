using Newtonsoft.Json;

namespace AdamSmithBot.Models
{
    public class TelegramResponse
    {
        [JsonProperty("method")]
        public string Method { get; set; }

        [JsonProperty("chat_id")]
        public long ChatId { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }
    }
}
