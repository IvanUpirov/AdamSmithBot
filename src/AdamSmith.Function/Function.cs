using AdamSmith.DataProviders;
using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;
using Newtonsoft.Json.Linq;
using System;
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
            string responseText = await GetDataProvider(query).GetDataAsync(query);

            var responseBody = new JObject
            {
                { "chat_id", update["message"]["chat"]["id"] },
                { "method", "sendMessage" },
                { "text", responseText }
            };

            return responseBody.ToString();
        }

        private IDataProvider GetDataProvider(string query)
        {
            return new CurrencyDataProvider();
        }
    }
}
