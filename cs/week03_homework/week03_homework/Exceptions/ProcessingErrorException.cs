using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace week03_homework.Exceptions
{
    public class ProcessingErrorException : Exception
    {
        public ProcessingErrorException(string message,Exception innerException):base(message,innerException)
        {
        }
        public ProcessingErrorException()
        {
        }
    }
}
