using FacadePattern.API.Settings.NotificationHandlerSettings;

namespace FacadePattern.API.Interfaces.Settings;

public interface INotificationHandler
{
    List<Notification> GetNotifications();
    bool HasNotifications();
    void AddNotification(string key, string message);
}
