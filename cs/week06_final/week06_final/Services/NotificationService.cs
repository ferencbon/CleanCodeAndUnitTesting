using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using week06_final.Abstraction.Clients;
using week06_final.Abstraction.Services;
using week06_final.Abstraction.Wrapper;
using week06_final.Exceptions;

namespace week06_final.Services
{
    public class NotificationService : INotificationService
    {
        private readonly ILoggerWrapper<NotificationService> _logger;
        private readonly IEnumerable<INotificationClient> _clients;

        public NotificationService(ILoggerWrapper<NotificationService> logger, IEnumerable<INotificationClient> clients)
        {
            _logger = logger;
            _clients = clients;
        }
        public async Task SendNotificationAsync(string message)
        {
            foreach (var notificationClient in _clients)
            {
                try
                {
                    await notificationClient.SendNotificationAsync(message);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex,ex.Message);
                    throw new NotificationServiceException("Error in SendNotificationAsync. See inner exception for details", ex);
                }
            }
        }
    }
}
