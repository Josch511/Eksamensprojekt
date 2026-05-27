namespace WebApp.Service
{
    public enum NotificationType { Success, Error }

    public record Notification(string Message, NotificationType Type);

    public class NotificationService
    {
        public event Action<Notification>? OnNotify;

        public void Success(string message) =>
            OnNotify?.Invoke(new Notification(message, NotificationType.Success));

        public void Error(string message) =>
            OnNotify?.Invoke(new Notification(message, NotificationType.Error));
    }
}