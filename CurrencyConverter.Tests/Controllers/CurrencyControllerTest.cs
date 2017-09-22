using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CurrencyConverter.Controllers;
using CurrencyConverter.Tests.Fakes;
using System.Web.Mvc;
using System.Collections.Generic;
using CurrencyConverter.Models;
using System.Web.Http.Results;

namespace CurrencyConverter.Tests.Controllers
{
    [TestClass]
    public class CurrencyControllerTest
    {
        [TestMethod]
        public void History_Perfect()
        {
            // Arrange
            var currency = "HUF";
            var fakeHistory = new List<CurrencyRate>() { new CurrencyRate() { Rate = 1, Timestamp = DateTime.Now } };
            var fakeRepository = new FakeCurrencyRespository();
            fakeRepository.history = fakeHistory;
            var controller = new CurrencyController(fakeRepository);

            // Act
            var historyTask = controller.History(currency);
            historyTask.Wait();
            var historyContent = (JsonResult<IEnumerable<CurrencyRate>>)historyTask.Result;
            var historyList = new List<CurrencyRate>(historyContent.Content);

            // Assert
            Assert.IsNotNull(historyContent);
            Assert.AreEqual(historyList.Count, 1);
            Assert.AreEqual(historyList[0].Rate, fakeHistory[0].Rate);
            Assert.AreEqual(historyList[0].Timestamp, fakeHistory[0].Timestamp);
        }
    }
}
