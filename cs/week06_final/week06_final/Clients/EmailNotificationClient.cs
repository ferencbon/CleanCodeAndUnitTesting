using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using week06_final.Abstraction.Clients;
using week06_final.Abstraction.Wrapper;
using week06_final.Exceptions;
using week06_final.Wrapper;

namespace week06_final.Clients
{
    public class EmailNotificationClient:INotificationClient
    {
        private readonly ILoggerWrapper<EmailNotificationClient> _logger;

        public EmailNotificationClient(ILoggerWrapper<EmailNotificationClient> logger)
        {
            _logger = logger;
        }
        public async Task SendNotificationAsync(string message)
        {
            try
            {
                if (string.IsNullOrEmpty(message))
                    throw new ArgumentNullException(nameof(message));

                Console.WriteLine(message);

                _logger.LogTrace("Message was sent!");
            }
            catch (ArgumentNullException ex)
            {
                _logger.LogError(ex, ex.Message);
                throw new NotificationException("Message cannot be sent. See inner exception for details.", ex);
            }
        }
    }
}
