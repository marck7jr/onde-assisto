namespace OndeAssisto.Common.Contracts
{
    public interface INotificationTriggeredEventArgs
    {
        public string Message { get; set; }
        public string Type { get; set; }
    }
}
