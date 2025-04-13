using Inv.Application.Interfaces;
using Inv.Application.Queries;
using Microsoft.Extensions.DependencyInjection;

namespace Inv.Application.Handlers;

public class GetAllWarehousesHandler(IWarehouseRepository repository)
{
    public async Task<List<WarehouseDto>> Handle(GetAllWarehousesQuery request, CancellationToken cancellationToken)
    {
        return (await repository.GetAllAsync(cancellationToken))
            .Select(w => new WarehouseDto((Guid)w.Id, w.Name)).ToList();
    }
}

public class GetWarehouseByIdHandler(IWarehouseRepository repository)
{
    public async Task<WarehouseWithInventoryDto?> Handle(GetWarehouseByIdQuery request,
        CancellationToken cancellationToken)
    {
        var warehouse = await repository.GetByIdAsync(new(request.Id), cancellationToken);
        if (warehouse is null) return null;

        return new(
            (Guid)warehouse.Id,
            warehouse.Name,
            warehouse.Inventory
                .Select(i => new InventoryItemDto((Guid)i.ItemId, i.ItemInfo.Sku, i.ItemInfo.Name, i.Quantity))
                .ToList());
    }
}

public static class WarehouseHandlerExtensions
{
    public static IServiceCollection AddWarehouseHandlers(this IServiceCollection services)
    {
        services.AddScoped<GetAllWarehousesHandler>();
        services.AddScoped<GetWarehouseByIdHandler>();

        return services;
    }
}