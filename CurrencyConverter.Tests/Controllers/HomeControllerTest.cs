using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CurrencyConverter;
using CurrencyConverter.Controllers;
using CurrencyConverter.Tests.Fakes;

namespace CurrencyConverter.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void Index_Perfect()
        {
            // Arrange
            var fakeCurrencyList = new List<string>() { "HUF", "EUR" };
            var fakeRepository = new FakeCurrencyRespository();
            fakeRepository.currencyList = fakeCurrencyList;
            HomeController controller = new HomeController(fakeRepository);

            // Act
            var resultTask = controller.Index();
            resultTask.Wait();
            var viewtContent = (ViewResult)resultTask.Result;
            var viewCurrencyList = viewtContent.ViewData.Values.First() as ICollection<string>;


            // Assert
            Assert.IsNotNull(viewtContent);
            Assert.AreEqual(viewCurrencyList.Count, fakeCurrencyList.Count);
            Assert.AreEqual(viewCurrencyList.ToList()[0], fakeCurrencyList[0]);
            Assert.AreEqual(viewCurrencyList.ToList()[1], fakeCurrencyList[1]);
        }
    }
}
