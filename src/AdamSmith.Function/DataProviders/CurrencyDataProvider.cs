using Newtonsoft.Json.Linq;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace AdamSmith.DataProviders
{
    class CurrencyDataProvider
    {
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

    class StartDataProvider
    {
        public string GetStartMessage() 
        {
            return "Список доступних валют доступний в меню. Приклад використання - /usd.";
        }
    }
}
