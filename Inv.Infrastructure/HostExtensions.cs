using Inv.Application.Interfaces;
using Inv.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Inv.Infrastructure;

public static class HostExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IWarehouseRepository, WarehouseRepository>();
        services.AddScoped<IItemRepository, ItemRepository>();
        
        return services;
    }
}