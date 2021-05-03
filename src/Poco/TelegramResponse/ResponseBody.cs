namespace AdamSmith.Poco.TelegramResponse
{
    public class ResponseBody
    {
        public string method { get; set; }
        public int chat_id { get; set; }
        public string text { get; set; }
    }
}
