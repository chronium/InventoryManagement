using Inv.Application.Handlers;
using Microsoft.Extensions.DependencyInjection;

namespace Inv.Application;

public static class HostExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        return services.AddScoped<GetAllItemsHandler>()
            .AddScoped<GetAllWarehousesHandler>();
    }
}