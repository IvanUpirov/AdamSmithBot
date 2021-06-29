using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Text;
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

            string query = update["message"]["text"].ToString().Replace("/", string.Empty);

            JObject responseBody = null;

            if (query.Length == 3)
            {
                var url = string.Format(
                    Environment.GetEnvironmentVariable("url"),
                    query,
                    DateTime.Now.ToString("yyyyMMdd"));

                using var client = new HttpClient();
                HttpResponseMessage response = await client.GetAsync(url);
                var content = await response.Content.ReadAsStringAsync();
                JToken rate = JArray.Parse(content)[0];

                responseBody = new JObject
                    {
                        { "chat_id", update["message"]["chat"]["id"] },
                        { "method", "sendMessage" },
                        { "text", $"Курс для {query} на {DateTime.Now:dd.MM.yyyy}: {rate["rate"]}" }
                    };
            }

            else
            {
                // your code here
                using var client = new HttpClient();
                string url = "https://webdata.pfts.ua:8484/sitedata/rest/ua/moreinformation/UA4000214498/security-info.csv";
                HttpResponseMessage response = await client.GetAsync(url);
                var bytes = await response.Content.ReadAsByteArrayAsync();
                var text = Encoding.UTF8.GetString(bytes);
                responseBody = new JObject();
            }

            return responseBody.ToString();
        }
    }
}
