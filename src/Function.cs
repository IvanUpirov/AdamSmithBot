using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;
using Newtonsoft.Json.Linq;
using System;
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
            JObject update = JObject.Parse(request.Body);

            var url = string.Format(
                Environment.GetEnvironmentVariable("url"),
                update["message"]["text"].ToString().Replace("/", string.Empty),
                DateTime.Now.ToString("yyyyMMdd"));

            using var client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(url);
            var content = await response.Content.ReadAsStringAsync();
            JToken rate = JArray.Parse(content)[0];

            var responseBody = new JObject
            {
                { "chat_id", update["message"]["chat"]["id"] },
                { "method", "sendMessage" },
                { "text", $"Курс для usd на {DateTime.Now:dd.MM.yyyy}: {rate["rate"]}" }
            };

            return responseBody.ToString();
        }
    }
}
