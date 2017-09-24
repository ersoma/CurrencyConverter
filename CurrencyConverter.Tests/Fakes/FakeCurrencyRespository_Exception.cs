using CurrencyConverter.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CurrencyConverter.Models;

namespace CurrencyConverter.Tests.Fakes
{
    class FakeCurrencyRespository_Exception : ICurrencyRepository
    {
        public string ExceptionText { get; set; }

        public Task<IEnumerable<string>> GetCurrencyList()
        {
            throw new Exception(ExceptionText);
        }

        public Task<IEnumerable<CurrencyRate>> GetHistoricalRatesForCurrency(string currency)
        {
            throw new Exception(ExceptionText);
        }

        public Task<decimal> GetLatestRateForCurrency(string currency)
        {
            throw new Exception(ExceptionText);
        }
    }
}
