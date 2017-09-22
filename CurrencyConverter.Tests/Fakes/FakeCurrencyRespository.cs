using CurrencyConverter.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CurrencyConverter.Models;

namespace CurrencyConverter.Tests.Fakes
{
    class FakeCurrencyRespository : ICurrencyRepository
    {
        public List<CurrencyRate> history { get; set; }
        public Dictionary<string, decimal> latestRates { get; set; }
        public List<string> currencyList { get; set; }

        public Task<IEnumerable<string>> GetCurrencyList()
        {
            return Task.FromResult<IEnumerable<string>>(currencyList);
        }

        public Task<IEnumerable<CurrencyRate>> GetHistoricalRatesForCurrency(string currency)
        {
            return Task.FromResult<IEnumerable<CurrencyRate>>(history);
        }

        public Task<decimal> GetLatestRateForCurrency(string currency)
        {
            return Task.FromResult<decimal>(latestRates[currency]);
        }
    }
}
