using OndeAssisto.Common.Contracts;
using System;

namespace OndeAssisto.Web.App.Services.Notification
{
    public class NotificationTriggeredEventArgs : EventArgs, INotificationTriggeredEventArgs
    {
        public string Message { get; set; }
        public string Type { get; set; }
    }
}
