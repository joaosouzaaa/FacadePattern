using FacadePattern.API.Data.Repositories;
using FacadePattern.API.Interfaces.Repositories;

namespace FacadePattern.API.DependencyInjection;

public static class RepositoriesDependencyInjection
{
    public static void AddRepositoriesDependencyInjection(this IServiceCollection services)
    {
        services.AddScoped<ICouponRepository, CouponRepository>();
        services.AddScoped<IInventoryRepository, InventoryRepository>();
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();
    }
}
