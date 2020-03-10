using OndeAssisto.Common.Contracts;
using System;
using System.Threading.Tasks;

namespace OndeAssisto.Web.App.Services.Notification
{
    public class NotificationService : INotificationService
    {
        public event EventHandler<INotificationTriggeredEventArgs> NotificationTriggered;

        public Task OnNotificationTriggeredAsync(string message, string type)
        {
            NotificationTriggered?.Invoke(this, new NotificationTriggeredEventArgs { Message = message, Type = type });
            return Task.CompletedTask;
        }
    }
}
