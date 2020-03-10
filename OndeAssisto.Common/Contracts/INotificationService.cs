using System;
using System.Threading.Tasks;

namespace OndeAssisto.Common.Contracts
{
    public interface INotificationService
    {
        public event EventHandler<INotificationTriggeredEventArgs> NotificationTriggered;

        public Task OnNotificationTriggeredAsync(string message, string type);
    }
}
