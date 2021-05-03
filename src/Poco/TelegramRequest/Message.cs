using System.Collections.Generic;
namespace AdamSmith.Poco.TelegramRequest
{
    public class Message
    {
        public int message_id { get; set; }
        public From from { get; set; }
        public Chat chat { get; set; }
        public int date { get; set; }
        public string text { get; set; }
        public List<Entity> entities { get; set; }
    }
}
