using CurrencyConverter.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace CurrencyConverter.Controllers
{
    public class HomeController : Controller
    {
        private ICurrencyRepository currencyRepository { get; set; }

        public HomeController(ICurrencyRepository currencyRepository)
        {
            this.currencyRepository = currencyRepository;
        }

        [HttpGet]
        public async Task<ActionResult> Index()
        {
            ViewBag.CurrencyList = await currencyRepository.GetCurrencyList();
            return View();
        }
    }
}