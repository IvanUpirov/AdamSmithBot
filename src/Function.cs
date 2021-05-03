using AdamSmith.Poco.Bank;
using AdamSmith.Poco.TelegramRequest;
using AdamSmith.Poco.TelegramResponse;
using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace AdamSmith
{
    public class Function
    {
        public async Task<string> FunctionHandler(APIGatewayProxyRequest request)
        {
            Update update = JsonConvert.DeserializeObject<Update>(request.Body);

            var url = string.Format(
                Environment.GetEnvironmentVariable("url"),
                update.message.text.Replace("/", string.Empty),
                DateTime.Now.ToString("yyyyMMdd"));

            using var client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(url);
            var content = await response.Content.ReadAsStringAsync();
            var rate = JsonConvert.DeserializeObject<CurrencyRate[]>(content).First();

            var responseBody = new ResponseBody
            {
                chat_id = update.message.chat.id,
                method = "sendMessage",
                text = $"Курс для usd на {DateTime.Now:dd.MM.yyyy}: {rate.rate}"
            };

            return JsonConvert.SerializeObject(responseBody);
        }
    }
}
