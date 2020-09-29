using Xunit;
using Newtonsoft.Json;
using AdamSmithBot.Models;
using Amazon.Lambda.APIGatewayEvents;

namespace AdamSmithBot.Tests
{
    public class FunctionTest
    {
        [Fact]
        public void TestToUpperFunction()
        {
            var function = new Function();

            TelegramUpdate telegramUpdate = new TelegramUpdate() 
            {
                Message = new TelegramMessage() 
                {
                    Chat = new TelegramChat() 
                    {
                        Id = 1
                    },
                    Text = "/usd"
                    }
            };

            string requestBody = JsonConvert.SerializeObject(telegramUpdate);
            APIGatewayProxyRequest getewayRequest = new APIGatewayProxyRequest() {Body = requestBody };
            
            var r = function.FunctionHandler(getewayRequest, null).GetAwaiter().GetResult();
        }
    }
}
