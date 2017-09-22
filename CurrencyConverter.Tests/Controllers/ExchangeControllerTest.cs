using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CurrencyConverter.Tests.Fakes;
using CurrencyConverter.Controllers;
using System.Web.Http.Results;
using System.Collections.Generic;

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
            Assert.AreEqual(convertContent.Content, 5);
        }
    }
}
