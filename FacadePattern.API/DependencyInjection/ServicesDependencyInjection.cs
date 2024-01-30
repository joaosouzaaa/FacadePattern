using FacadePattern.API.Interfaces.Services;
using FacadePattern.API.Services;

namespace FacadePattern.API.DependencyInjection;

public static class ServicesDependencyInjection
{
    public static void AddServicesDependencyInjection(this IServiceCollection services)
    {
        services.AddScoped<ICouponService, CouponService>(); 
        services.AddScoped<IEcommerceFacade, EcommerceFacade>();
        services.AddScoped<IInventoryService, InventoryService>();
        services.AddScoped<IOrderService,  OrderService>();
        services.AddScoped<IProductOrderService,  ProductOrderService>();
        services.AddScoped<IProductService, ProductService>();
    }
}
