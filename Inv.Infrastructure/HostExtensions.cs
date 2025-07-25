using Inv.Application.Interfaces;
using Inv.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Inv.Infrastructure;

public static class HostExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        return services.AddScoped<IWarehouseRepository, WarehouseRepository>()
            .AddScoped<IItemRepository, ItemRepository>();
    }
}