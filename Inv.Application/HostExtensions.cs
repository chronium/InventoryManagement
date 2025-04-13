using Inv.Application.Handlers;
using Microsoft.Extensions.DependencyInjection;

namespace Inv.Application;

public static class HostExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddWarehouseHandlers();
        services.AddItemHandlers();

        return services;
    }
}