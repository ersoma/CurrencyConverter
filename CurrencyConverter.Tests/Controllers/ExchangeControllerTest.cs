using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CurrencyConverter.Tests.Fakes;
using CurrencyConverter.Controllers;
using System.Web.Http.Results;
using System.Collections.Generic;
using System.Web.Http;

namespace CurrencyConverter.Tests.Controllers
{
    [TestClass]
    public class ExchangeControllerTest
    {
        [TestMethod]
        public void Convert_Perfect()
        {
            // Arrange
            var fakeLatestRates = new Dictionary<string, decimal>() { { "HUF", 2 }, { "USD", 1 } };
            var fakeRepository = new FakeCurrencyRespository();
            fakeRepository.latestRates = fakeLatestRates;
            var controller = new ExchangeController(fakeRepository);

            // Act
            var convertTask = controller.Convert("10", "HUF", "USD");
            convertTask.Wait();
            var convertContent = (JsonResult<Decimal>)convertTask.Result;

            // Assert
            Assert.IsNotNull(convertContent);
            Assert.AreEqual(5, convertContent.Content);
        }

        [TestMethod]
        public void Convert_Exception()
        {
            // Arrange
            var fakeRepository = new FakeCurrencyRespository_Exception();
            var controller = new ExchangeController(fakeRepository);

            try
            {
                // Act
                var convertTask = controller.Convert("10", "HUF", "USD");
                convertTask.Wait();
                throw new Exception("No exception is thrown by controller.");
            }
            catch (AggregateException ex)
            {
                // Assert
                Assert.AreEqual(typeof(HttpResponseException), ex.InnerException.GetType());
                var innerEx = ex.InnerException as HttpResponseException;
                var innerContent = innerEx.Response.Content.ReadAsStringAsync();
                innerContent.Wait();
                Assert.AreEqual(Messages.ConvertErrorMessage, innerContent.Result);
            }
        }
    }
}
