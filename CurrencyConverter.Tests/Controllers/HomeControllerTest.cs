using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CurrencyConverter;
using CurrencyConverter.Controllers;
using CurrencyConverter.Tests.Fakes;
using System.Web.Http;

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
            Assert.AreEqual(fakeCurrencyList.Count, viewCurrencyList.Count);
            Assert.AreEqual(fakeCurrencyList[0], viewCurrencyList.ToList()[0]);
            Assert.AreEqual(fakeCurrencyList[1], viewCurrencyList.ToList()[1]);
        }

        [TestMethod]
        public void Index_Exception()
        {
            // Arrange
            var fakeExceptionText = "TEST";
            var fakeRepository = new FakeCurrencyRespository_Exception();
            fakeRepository.ExceptionText = fakeExceptionText;
            HomeController controller = new HomeController(fakeRepository);

            try
            {
                // Act
                var resultTask = controller.Index();
                resultTask.Wait();
                throw new Exception("No exception is thrown by controller.");
            }
            catch (AggregateException ex)
            {
                // Assert
                Assert.AreEqual(typeof(Exception), ex.InnerException.GetType());
                Assert.AreEqual(fakeExceptionText, ex.InnerException.Message);
            }
        }
    }
}
