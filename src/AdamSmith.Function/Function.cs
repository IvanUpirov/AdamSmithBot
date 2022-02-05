using AdamSmith.DataProviders;
using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;
using Amazon.Lambda.Serialization.SystemTextJson;
using Newtonsoft.Json.Linq;
using System;
using System.Threading.Tasks;

[assembly: LambdaSerializer(typeof(DefaultLambdaJsonSerializer))]

namespace AdamSmith
{
    public class Function
    {
        public async Task<string> FunctionHandler(APIGatewayProxyRequest request)
        {
            try
            {
                JObject update = JObject.Parse(request.Body);
                string query = update["message"]["text"].ToString().Replace("/", string.Empty);

                string responseText = query switch
                {
                    "start" => new StartDataProvider().GetStartMessage(),
                    _ when query.Length == 3 => await new CurrencyDataProvider().GetDataAsync(query),
                    _ => throw new Exception($"Unexpected query: {query}")
                };

                var responseBody = new JObject
                {
                    { "chat_id", update["message"]["chat"]["id"] },
                    { "method", "sendMessage" },
                    { "text", responseText }
                };

                return responseBody.ToString();
            }
            catch (Exception e)
            {
                LambdaLogger.Log(e.Message);
                LambdaLogger.Log(request.Body);
                return string.Empty;
            }
        }
    }
}
