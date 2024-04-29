using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace week02_homework
{
    public class CurrencyConverter
    {
        private readonly IExchangeRateService _exchangeRateService;
        private const int FixedAmount = 100;

        public CurrencyConverter(IExchangeRateService exchangeRateService)
        {
            _exchangeRateService = exchangeRateService;
        }

        public double Convert(double amount, string fromCurrency, string toCurrency)
        {
            ValidateAmount(amount);
            var exchangeRate = GetExchangeRate(fromCurrency, toCurrency);
            ValidateExchangeRate(exchangeRate);
            return amount * exchangeRate;
        }

        public string GenerateConversionReport(string fromCurrency, string toCurrency, DateTime startDate, DateTime endDate)
        {
            var conversions = "";

            var currentDate = startDate;

            while (currentDate <= endDate)
            {
                var exchangeRate = GetExchangeRate(fromCurrency, toCurrency);
                ValidateExchangeRate(exchangeRate);
                conversions += CalculateConversion(exchangeRate, currentDate) + "\n";
                currentDate = currentDate.AddDays(1);
            }

            return $"Conversion Report:\n{conversions}";
        }
        

        private double GetExchangeRate(string fromCurrency, string toCurrency)
        {
            if (string.IsNullOrWhiteSpace(fromCurrency))
                throw new ArgumentNullException(nameof(fromCurrency));
            if (string.IsNullOrWhiteSpace(toCurrency))
                throw new ArgumentNullException(nameof(toCurrency));
            
            try
            {
                var result=_exchangeRateService.GetExchangeRate(fromCurrency, toCurrency);
                return result;

            }
            catch (Exception ex)
            {
                throw new ExchangeRateServiceException(ex);
            }
        }

        private double CalculateConversion(double exchangeRate, DateTime currentDate)
        {
            return FixedAmount * exchangeRate; // Assume a fixed amount for simplicity
        }

        private void ValidateExchangeRate(double exchangeRate)
        {
            if (exchangeRate <= 0)
            {
                throw new ArgumentException("Unable to fetch exchange rate.");
            }
        }

        private void ValidateAmount(double amount)
        {
            if (amount <= 0)
            {
                throw new ArgumentException("Invalid amount input.");
            }
        }
    }
}
