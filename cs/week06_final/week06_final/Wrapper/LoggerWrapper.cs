using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using week06_final.Abstraction.Wrapper;

namespace week06_final.Wrapper
{
    public class LoggerWrapper<T> : ILoggerWrapper<T>
    {
        private readonly ILogger<T> _logger;

        public LoggerWrapper(ILogger<T> logger)
        {
            _logger = logger;
        }

        public void LogError(Exception exception, string message)
        {
            _logger.LogError(exception, message);
        }

        public void LogTrace(string message)
        {
            _logger.LogTrace(message);
        }
    }
}
