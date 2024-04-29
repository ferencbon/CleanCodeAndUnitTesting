using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace week03_homework.Exceptions
{
    public class InvalidImageException : Exception
    {
        public InvalidImageException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public InvalidImageException(string message) : base(message)
        {
        }

        public InvalidImageException()
        {
        }
    }
}
