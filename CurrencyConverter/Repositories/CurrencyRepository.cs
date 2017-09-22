using CurrencyConverter.Models.Eurofxref;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Xml.Serialization;
using System.Threading.Tasks;
using CurrencyConverter.Models;
using System.Globalization;
using System.Configuration;

namespace CurrencyConverter.Repositories
{
    public class CurrencyRepository : ICurrencyRepository
    {
        private List<Currency> currencyList { get; set; }
        private DateTime lastUpdate { get; set; }


        public async Task<IEnumerable<string>> GetCurrencyList()
        {
            await UpdateCurrencies();
            var mappedCurrencies = currencyList.Select(x => x.Name);
            return mappedCurrencies;
        }

        public async Task<decimal> GetLatestRateForCurrency(string currency)
        {
            await UpdateCurrencies();
            var rate = currencyList.Single(x => x.Name.ToLower() == currency.ToLower()).Rates.OrderByDescending(x => x.Timestamp).First().Rate;
            return rate;
        }

        public async Task<IEnumerable<CurrencyRate>> GetHistoricalRatesForCurrency(string currency)
        {
            await UpdateCurrencies();
            var rates = currencyList.Single(x => x.Name.ToLower() == currency.ToLower()).Rates.OrderBy(x => x.Timestamp);
            return rates;
        }

        private async Task UpdateCurrencies()
        {
            var cacheDurationInMinutes = Convert.ToInt32(ConfigurationManager.AppSettings["cacheDurationInMinutes"]);
            var isCacheExpired = DateTime.Now.Subtract(lastUpdate) > TimeSpan.FromMinutes(cacheDurationInMinutes);
            if (currencyList != null && isCacheExpired == false)
            {
                return;
            }

            var rawCurrencyList = await GetXmlFromSource();
            var envelope = await ParseHttpResponseToEnvelope(rawCurrencyList);
            currencyList = ParseEnvelopeToCurrency(envelope);
            lastUpdate = DateTime.Now;
        }

        private async Task<HttpResponseMessage> GetXmlFromSource()
        {
            HttpResponseMessage response;
            using (var client = new HttpClient())
            {
                var path = ConfigurationManager.AppSettings["CurrencyDatasourceUrl"];
                response = await client.GetAsync(path);
            }

            if (response.IsSuccessStatusCode == false)
            {
                throw new Exception(Messages.XmlDownloadError);
            }
            return response;
        }

        private async Task<Envelope> ParseHttpResponseToEnvelope(HttpResponseMessage rawCurrencyList)
        {
            var stringCurrencyList = await rawCurrencyList.Content.ReadAsStringAsync();
            var serializer = new XmlSerializer(typeof(Envelope));
            Envelope result;
            using (TextReader reader = new StringReader(stringCurrencyList))
            {
                result = (Envelope)serializer.Deserialize(reader);
            }
            return result;
        }

        private List<Currency> ParseEnvelopeToCurrency(Envelope envelope)
        {
            var currencyResult = new List<Currency>();
            foreach (var TimestampWithList in envelope.Cube.Cube)
            {
                var timestamp = Convert.ToDateTime(TimestampWithList.Time);
                foreach (var RateWithCurrency in TimestampWithList.Cube)
                {
                    var name = RateWithCurrency.Currency;
                    var rate = Convert.ToDecimal(RateWithCurrency.Rate, new CultureInfo("en-US"));

                    var currentCurency = currencyResult.SingleOrDefault(x => x.Name == name);
                    if(currentCurency == null)
                    {
                        currentCurency = new Currency { Name = name, Rates = new List<CurrencyRate>() };
                        currencyResult.Add(currentCurency);
                    }
                    currentCurency.Rates.Add(new CurrencyRate() { Rate = rate, Timestamp = timestamp });
                }
            }
            return currencyResult;
        }
    }
}