using Newtonsoft.Json;

namespace AdamSmithBot.Models
{
    public class TelegramUpdate
    {
        [JsonProperty("message")]
        public TelegramMessage Message { get; set; }
    }
}
