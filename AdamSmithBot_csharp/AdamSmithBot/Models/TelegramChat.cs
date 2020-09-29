using Newtonsoft.Json;

namespace AdamSmithBot.Models
{
    public class TelegramChat
    {
        [JsonProperty("id")]
        public long Id { get; set; }
    }
}
