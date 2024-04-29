using Microsoft.VisualStudio.TestTools.UnitTesting;
using week02_homework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using Snapshooter.MSTest;
using week02_homeworkTests;

namespace week02_homework.Tests
{
    [TestClass()]
    public class CurrencyConverterTests
    {

        private Mock<IExchangeRateService> _mockExchangeRateService;
        private CurrencyConverter _currencyConverter;
        [TestInitialize]
        public void TestInitialize()
        {
            _mockExchangeRateService = new Mock<IExchangeRateService>();
            _currencyConverter = new CurrencyConverter(_mockExchangeRateService.Object);
        }
        
        [DataTestMethod]
        [TestCategory("Convert|Happy Flow")]
        [DataRow(100, "USD", "EUR", 0.85, 85)]
        [DataRow(200, "USD", "EUR", 0.85, 170)]
        [DataRow(300, "USD", "EUR", 0.85, 255)]
        [DataRow(400, "USD", "EUR", 0.85, 340)]
        [DataRow(500, "USD", "EUR", 0.85, 425)]
        public void GivenAmountAndCurrencies_WhenConvert_ThenReturnsExpectedResult(double amount, string fromCurrency, string toCurrency, double exchangeRate, double expectedResult)
        {
            // Arrange
            _mockExchangeRateService.Setup(x => x.GetExchangeRate(fromCurrency, toCurrency)).Returns(exchangeRate);
            
            // Act
            var result = _currencyConverter.Convert(amount, fromCurrency, toCurrency);
            
            // Assert
            Assert.AreEqual(expectedResult, result, 0.001);
            _mockExchangeRateService.Verify(x=>x.GetExchangeRate(fromCurrency,toCurrency), Times.Once());
        }
        
        [DataTestMethod]
        [TestCategory("ConversionReport|Happy Flow")]
        [DataRow("USD", "EUR", "2022/01/01", "2022/01/05", 1.2)]
        [DataRow("USD", "GBP", "2022/02/01", "2022/02/05", 0.8)]
        [DataRow("EUR", "USD", "2022/03/01", "2022/03/05", 1.3)]
        [DataRow("EUR", "GBP", "2022/04/01", "2022/04/05", 0.9)]
        [DataRow("GBP", "USD", "2022/05/01", "2022/05/05", 1.4)]
        public void GivenValidCurrenciesAndDates_WhenGenerateConversionReport_ThenCorrectReportIsReturned(
            string fromCurrency, string toCurrency, string start, string end, double exchangeRate)
        {
            // Arrange
            var startDate = DateTime.Parse(start);
            var endDate = DateTime.Parse(end);
            _mockExchangeRateService.Setup(x => x.GetExchangeRate(It.IsAny<string>(), It.IsAny<string>())).Returns(exchangeRate);
            
            // Act
            var actualReport = _currencyConverter.GenerateConversionReport(fromCurrency, toCurrency, startDate, endDate);
            
            // Assert
            Snapshot.Match(actualReport,$"CurrencyReport{fromCurrency}{toCurrency}{start}{end}{exchangeRate}".RemoveIllegalCharacters());
        }

        [DataTestMethod]
        [TestCategory("Convert|Error Flow")]
        [DataRow(-100, "USD", "EUR", 5,0)]
        [DataRow(200, "USD", "EUR", -1000.85,1) ]
        [DataRow(-300, "USD", "EUR", -0.85,0)]
        public void GivenInvalidAmountOrExchangeRate_WhenConvert_ThenThrowsException(double amount, string fromCurrency, string toCurrency, double exchangeRate, int dependencycallCount)
        {
            // Arrange
            _mockExchangeRateService.Setup(x => x.GetExchangeRate(fromCurrency, toCurrency)).Returns(exchangeRate);
            
            // Act & Assert
            Assert.ThrowsException<ArgumentException>(
                () => _currencyConverter.Convert(amount, fromCurrency, toCurrency));
            _mockExchangeRateService.Verify(x => x.GetExchangeRate(fromCurrency, toCurrency), Times.Exactly(dependencycallCount));
        }
        
        [DataTestMethod]
        [TestCategory("ConversionReport|Error Flow")]
        [DataRow("USD", "EUR", "2022/01/01", "2022/01/05",-10)]
        [DataRow("USD", "GBP", "2022/02/01", "2022/02/05",-1000)]
        [DataRow("EUR", "USD", "2022/03/01", "2022/03/05",-0.66)]
        [DataRow("EUR", "GBP", "2022/04/01", "2022/04/05",0)]
        public void GivenInvalidExchangeRate_WhenGenerateConversionReport_ThenThrowsException(string fromCurrency, string toCurrency, string start, string end, double exchangeRate)
        {
            // Arrange
            var startDate = DateTime.Parse(start);
            var endDate = DateTime.Parse(end);
            _mockExchangeRateService.Setup(x => x.GetExchangeRate(fromCurrency, toCurrency)).Returns(exchangeRate);
            
            // Act & Assert
            Assert.ThrowsException<ArgumentException>(() => _currencyConverter.GenerateConversionReport(fromCurrency, toCurrency, startDate, endDate));
            Assert.AreEqual(_mockExchangeRateService.Object.GetExchangeRate(fromCurrency, toCurrency),
                exchangeRate);
            _mockExchangeRateService.Verify(x => x.GetExchangeRate(fromCurrency, toCurrency), Times.Exactly(2));
        }

        [DataTestMethod]
        [TestCategory("ConversionReport|Error Flow")]
        [DataRow("", "EUR", "2022/01/01", "2022/01/05", -10)]
        [DataRow("USD", "", "2022/02/01", "2022/02/05", -1000)]
        [DataRow("", "", "2022/03/01", "2022/03/05", -0.66)]
        public void GivenEmptycurrency_WhenGenerateConversionReport_ThenThrowsException(string fromCurrency, string toCurrency, string start, string end, double exchangeRate)
        {
            // Arrange
            var startDate = DateTime.Parse(start);
            var endDate = DateTime.Parse(end);
            _mockExchangeRateService.Setup(x => x.GetExchangeRate(fromCurrency, toCurrency)).Returns(exchangeRate);

            // Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() => _currencyConverter.GenerateConversionReport(fromCurrency, toCurrency, startDate, endDate));
            _mockExchangeRateService.Verify(x => x.GetExchangeRate(fromCurrency, toCurrency), Times.Exactly(0));
        }

        [DataTestMethod]
        [TestCategory("Convert|Error Flow")]
        [DataRow(150, "USD", "EUR")]
        [DataRow(100, "USD", "EUR")]
        public void GivenExceptionFromdependency_WhenConvert_ThenThrowsException(double amount, string fromCurrency, string toCurrency)
        {
            // Arrange
            _mockExchangeRateService.Setup(x => x.GetExchangeRate(fromCurrency, toCurrency)).Throws<Exception>();
            
            // Act & Assert
            Assert.ThrowsException<ExchangeRateServiceException>(() => _currencyConverter.Convert(amount, fromCurrency, toCurrency));
            _mockExchangeRateService.Verify(x=>x.GetExchangeRate(It.IsAny<string>(),It.IsAny<string>()),Times.Once);
        }
        [DataTestMethod]
        [TestCategory("Convert|Error Flow")]
        [DataRow(150, "", "EUR")]
        [DataRow(100, "USD", "")]
        [DataRow(100, "", "")]
        public void GivenEmptyCurrency_WhenConvert_ThenThrowsException(double amount, string fromCurrency, string toCurrency)
        {
            // Arrange
            _mockExchangeRateService.Setup(x => x.GetExchangeRate(fromCurrency, toCurrency)).Throws<Exception>();

            // Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() => _currencyConverter.Convert(amount, fromCurrency, toCurrency));
            _mockExchangeRateService.Verify(x => x.GetExchangeRate(It.IsAny<string>(), It.IsAny<string>()), Times.Exactly(0));
        }

        [DataTestMethod]
        [TestCategory("ConversionReport|Error Flow")]
        [DataRow("USD", "EUR", "2022/01/01", "2022/01/05")]
        [DataRow("USD", "GBP", "2022/02/01", "2022/02/05")]
        public void GivenExceptionFromdependency_WhenGenerateConversionReport_ThenThrowsException(string fromCurrency, string toCurrency, string start, string end)
        {
            // Arrange
            var startDate = DateTime.Parse(start);
            var endDate = DateTime.Parse(end);
            _mockExchangeRateService.Setup(x => x.GetExchangeRate(It.IsAny<string>(), It.IsAny<string>())).Throws<Exception>();
            
            // Act & Assert
            Assert.ThrowsException<ExchangeRateServiceException>( () => _currencyConverter.GenerateConversionReport(fromCurrency, toCurrency, startDate, endDate));
            _mockExchangeRateService.Verify(x=>x.GetExchangeRate(It.IsAny<string>(),It.IsAny<string>()),Times.Once);

        }
    }
}