using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace week06_final.Abstraction.Wrapper
{
    public interface ILoggerWrapper<T>
    {
        void LogError(Exception exception, string message);
        void LogTrace(string message);
    }

}
