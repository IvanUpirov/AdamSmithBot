using Amazon.Lambda.APIGatewayEvents;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Threading.Tasks;

namespace AdamSmith.Tests
{
    [TestClass]
    public class Integration
    {
        [TestInitialize]
        public void Initialize() 
        {
            Environment.SetEnvironmentVariable("url", "https://bank.gov.ua/NBUStatService/v1/statdirectory/exchange?valcode={0}&date={1}&json");
        }

        [TestMethod]
        public async Task EndToEnd()
        {
            var sut = new Function();

            var chat = new JObject
            {
                { "id", 1 }
            };

            var message = new JObject
            {
                { "chat", chat },
                { "text", "/usd" }
            };

            var update = new JObject
            {
                { "message", message }
            };


            var request = new APIGatewayProxyRequest
            {
                Body = JsonConvert.SerializeObject(update)
            };

            await sut.FunctionHandler(request);
        }

        [TestCleanup]
        public void Cleanup()
        {
            Environment.SetEnvironmentVariable("url", null);
        }
    }
}
