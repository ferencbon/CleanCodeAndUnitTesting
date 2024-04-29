using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace week02_homework
{
    public interface IExchangeRateService
    {
        public double GetExchangeRate(string fromCurrency, string toCurrency);
    }
}
