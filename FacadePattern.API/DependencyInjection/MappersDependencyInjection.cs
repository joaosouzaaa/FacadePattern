using FacadePattern.API.Interfaces.Mappers;
using FacadePattern.API.Mappers;

namespace FacadePattern.API.DependencyInjection;

public static class MappersDependencyInjection
{
    public static void AddMappersDependencyInjection(this IServiceCollection services)
    {
        services.AddScoped<ICouponMapper, CouponMapper>();
        services.AddScoped<IInventoryMapper, InventoryMapper>();
        services.AddScoped<IOrderMapper, OrderMapper>();
        services.AddScoped<IProductMapper, ProductMapper>();
        services.AddScoped<IProductOrderMapper, ProductOrderMapper>();
    }
}
