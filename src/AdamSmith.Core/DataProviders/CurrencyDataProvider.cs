using Newtonsoft.Json.Linq;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace AdamSmith.Core.DataProviders
{
    class CurrencyDataProvider : IDataProvider
    {
        public string GetData(string input)
        {
            throw new NotImplementedException();
        }

        public async Task<string> GetDataAsync(string input)
        {
            var url = string.Format(
                Environment.GetEnvironmentVariable("currency_url"),
                input,
                DateTime.Now.ToString("yyyyMMdd"));

            using var client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(url);
            var content = await response.Content.ReadAsStringAsync();
            JToken rate = JArray.Parse(content).FirstOrDefault();

            return rate != null
                ? $"Курс для {input} на {DateTime.Now:dd.MM.yyyy}: {rate["rate"]}"
                : $"У мене немає курсу для цієї валюти";
        }
    }
}
