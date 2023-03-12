using Dal;
using Dal.Interfaces;
using Services.Interfaces;
using Services.Services;

namespace WebApi;

public static class IocConfig
{
    public static IServiceCollection AddUnitOfWork(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        return services;
    }

    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<ICuisineCategoryService, CuisineCategoryService>();
        services.AddScoped<IGroceryService, GroceryService>();
        services.AddScoped<IRecipeService, RecipeService>();
        services.AddScoped<IOrderService, OrderService>();

        return services;
    }
}