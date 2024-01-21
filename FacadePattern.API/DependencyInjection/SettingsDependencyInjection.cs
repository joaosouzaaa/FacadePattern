using FacadePattern.API.Interfaces.Settings;
using FacadePattern.API.Settings.NotificationHandlerSettings;

namespace FacadePattern.API.DependencyInjection;

public static class SettingsDependencyInjection
{
    public static void AddSettingsDependencyInjection(this IServiceCollection services)
    {
        services.AddScoped<INotificationHandler, NotificationHandler>();
    }
}
