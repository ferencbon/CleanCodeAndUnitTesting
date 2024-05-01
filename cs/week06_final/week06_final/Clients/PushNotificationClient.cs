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
    public class PushNotificationClient:INotificationClient
    {
        private readonly ILoggerWrapper<PushNotificationClient> _logger;

        public PushNotificationClient(ILoggerWrapper<PushNotificationClient> logger)
        {
            _logger = logger;
        }

        public async Task SendNotificationAsync(string message)
        {
            try
            {
                ArgumentException.ThrowIfNullOrWhiteSpace(message);
               
                Console.WriteLine(message);
                
                _logger.LogTrace("Message was sent!");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw new NotificationException("Message cannot be sent. See inner exception for details.", ex);
            }
        }
    }
}
