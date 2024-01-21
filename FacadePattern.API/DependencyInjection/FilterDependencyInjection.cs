using FacadePattern.API.Filters;

namespace FacadePattern.API.DependencyInjection;

public static class FilterDependencyInjection
{
    public static void AddFilterDependencyInjection(this IServiceCollection services)
    {
        services.AddScoped<NotificationFilter>();
        services.AddMvc(options => options.Filters.AddService<NotificationFilter>());
    }
}
