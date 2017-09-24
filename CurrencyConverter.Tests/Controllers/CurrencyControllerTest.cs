using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CurrencyConverter.Controllers;
using CurrencyConverter.Tests.Fakes;
using System.Web.Mvc;
using System.Collections.Generic;
using CurrencyConverter.Models;
using System.Web.Http.Results;
using System.Web.Http;

namespace CurrencyConverter.Tests.Controllers
{
    [TestClass]
    public class CurrencyControllerTest
    {
        [TestMethod]
        public void History_Perfect()
        {
            // Arrange
            var currency = "ANY";
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
            Assert.AreEqual(fakeHistory.Count, historyList.Count);
            Assert.AreEqual(fakeHistory[0].Rate, historyList[0].Rate);
            Assert.AreEqual(fakeHistory[0].Timestamp, historyList[0].Timestamp);
        }

        [TestMethod]
        public void History_Exception()
        {
            // Arrange
            var currency = "ANY";
            var fakeRepository = new FakeCurrencyRespository_Exception();
            var controller = new CurrencyController(fakeRepository);

            try
            {
                // Act
                var historyTask = controller.History(currency);
                historyTask.Wait();
                throw new Exception("No exception is thrown by controller.");
            }
            catch(AggregateException ex)
            {
                // Assert
                Assert.AreEqual(typeof(HttpResponseException), ex.InnerException.GetType());
                var innerEx = ex.InnerException as HttpResponseException;
                var innerContent = innerEx.Response.Content.ReadAsStringAsync();
                innerContent.Wait();
                Assert.AreEqual(Messages.HistoryErrorMessage, innerContent.Result);
            }
        }
    }
}
