﻿using AdamSmith.DataProviders;
using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;
using Newtonsoft.Json.Linq;
using System;
using System.Threading.Tasks;

[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

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
                string responseText = await GetDataProvider(query).GetDataAsync(query);

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

        private IDataProvider GetDataProvider(string query)
        {
            return new CurrencyDataProvider();
        }
    }
}
