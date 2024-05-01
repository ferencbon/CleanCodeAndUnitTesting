using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace week06_final.Abstraction.Clients
{
    public interface INotificationClient
    {
        public Task SendNotificationAsync(string message);
    }
}
