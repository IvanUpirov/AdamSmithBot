using Amazon.Lambda.Core;
using Amazon.Lambda.APIGatewayEvents;
using Newtonsoft.Json;
using AdamSmithBot.Models;
using System.Net.Http;
using System.Threading.Tasks;


// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace AdamSmithBot
{
    public class Function
    {
        public async Task<APIGatewayProxyResponse> FunctionHandler(APIGatewayProxyRequest getewayRequest, ILambdaContext context)
        {
            var update = JsonConvert.DeserializeObject<TelegramUpdate>(getewayRequest.Body);
            var message = update.Message;
            var currencyCode = message.Text.Replace("/", string.Empty);
            var url = $"http://bank.gov.ua/NBUStatService/v1/statdirectory/exchange?valcode={currencyCode}&json";

            HttpClientHandler clientHandler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
            };
            HttpClient client = new HttpClient(clientHandler);

            var nbuResponseRaw = await client.GetAsync(url);
            var nbuResponseText = await nbuResponseRaw.Content.ReadAsStringAsync();
            var nbuResponse = JsonConvert.DeserializeObject<NbuResponse[]>(nbuResponseText)[0];

            var responseBody = new TelegramResponse()
            {
                ChatId = message.Chat.Id,
                Method = "sendMessage",
                Text = $"Курс для {currencyCode} на {nbuResponse.ExchangeDate}: {nbuResponse.Rate}"
            };

            return new APIGatewayProxyResponse()
            {
                StatusCode = 200,
                Body = JsonConvert.SerializeObject(responseBody)
            };
        }
    }
}
