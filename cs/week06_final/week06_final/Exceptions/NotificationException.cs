using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace week06_final.Exceptions
{
    public class NotificationException : Exception
    {
        public NotificationException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
