using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace week06_final.Exceptions
{
    public class FinancialApiException : Exception
    {
        public FinancialApiException(string message, Exception exception) : base(message, exception)
        {
        }
    }
}
