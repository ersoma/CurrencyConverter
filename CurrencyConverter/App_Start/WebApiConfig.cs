using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace CurrencyConverter
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Formatters.XmlFormatter.UseXmlSerializer = true;

            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "Convert",
                routeTemplate: "api/convert/{fromAmount}/{fromCurrency}/to/{toCurrency}",
                defaults: new { controller = "Exchange", action = "Convert" }
            );

            config.Routes.MapHttpRoute(
                name: "History",
                routeTemplate: "api/history/{currency}",
                defaults: new { controller = "Currency", action = "History" }
            );
        }
    }
}
