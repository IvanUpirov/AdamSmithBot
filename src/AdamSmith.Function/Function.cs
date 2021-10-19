using AdamSmith.Core;
using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;
using Amazon.Lambda.Serialization.SystemTextJson;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;

[assembly: LambdaSerializer(typeof(DefaultLambdaJsonSerializer))]

namespace AdamSmith
{
    public class Function
    {
        public async Task<string> FunctionHandler(APIGatewayProxyRequest request)
        {
            var update = JObject.Parse(request.Body);
            var query = update["message"]["text"].ToString().Replace("/", string.Empty);
            var result = await new AdamSmithBot().GetDataAsync(query);
            result["chat_id"] = update["message"]["chat"]["id"];
            return result.ToString();
        }
    }
}
