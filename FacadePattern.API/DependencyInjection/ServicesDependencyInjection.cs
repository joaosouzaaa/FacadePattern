using FacadePattern.API.Interfaces.Services;
using FacadePattern.API.Services;

namespace FacadePattern.API.DependencyInjection;

public static class ServicesDependencyInjection
{
    public static void AddServicesDependencyInjection(this IServiceCollection services)
    {
        services.AddScoped<IProductService, ProductService>();
    }
}
