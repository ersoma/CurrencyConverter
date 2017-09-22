using CurrencyConverter.Repositories;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace CurrencyConverter.Controllers
{
    public class ExchangeController : ApiController
    {
        private ICurrencyRepository currencyRepository { get; set; }

        public ExchangeController(ICurrencyRepository currencyRepository)
        {
            this.currencyRepository = currencyRepository;
        }

        [HttpGet]
        public async Task<IHttpActionResult> Convert(string fromAmount, string fromCurrency, string toCurrency)
        {
            try
            {
                var fromRate = await currencyRepository.GetLatestRateForCurrency(fromCurrency);
                var toRate = await currencyRepository.GetLatestRateForCurrency(toCurrency);
                var amount = System.Convert.ToDecimal(fromAmount, new CultureInfo("en-US"));
                var result = amount / fromRate * toRate;
                return await Task.FromResult(Json(result));
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
                var response = new HttpResponseMessage(HttpStatusCode.InternalServerError){ Content = new StringContent(Messages.ConvertErrorMessage) };
                throw new HttpResponseException(response);
            }
            
        }

    }
}