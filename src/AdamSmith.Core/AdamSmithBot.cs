using AdamSmith.Core.DataProviders;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;

namespace AdamSmith.Core
{
    public class AdamSmithBot
    {
        public async Task<JObject> GetDataAsync(string message) 
        {

            string responseText = await GetDataProvider(message).GetDataAsync(message);

            var responseBody = new JObject
            {
                { "method", "sendMessage" },
                { "text", responseText }
            };

            return responseBody;
        }

        private IDataProvider GetDataProvider(string query)
        {
            return new CurrencyDataProvider();
        }
    }
}
