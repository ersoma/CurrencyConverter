using CurrencyConverter.Models;
using CurrencyConverter.Models.Eurofxref;
using CurrencyConverter.Repositories;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Xml.Serialization;

namespace CurrencyConverter.Controllers
{
    public class CurrencyController : ApiController
    {
        private ICurrencyRepository currencyRepository { get; set; }

        public CurrencyController(ICurrencyRepository currencyRepository)
        {
            this.currencyRepository = currencyRepository;
        }

        [HttpGet]
        public async Task<IHttpActionResult> History(string currency)
        {
            try
            {
                var result = await currencyRepository.GetHistoricalRatesForCurrency(currency);
                return await Task.FromResult(Json(result));
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
                var response = new HttpResponseMessage(HttpStatusCode.InternalServerError){ Content = new StringContent(Messages.HistoryErrorMessage) };
                throw new HttpResponseException(response);
            }            
        }

    }
}
