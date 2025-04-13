using Inv.Application.Commands;
using Inv.Application.Interfaces;
using Inv.Application.Queries;
using Inv.Domain.Entities;
using Inv.Domain.Entities.IdTypes;
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
        var warehouse = await repository.GetByIdAsync(request.Id, cancellationToken);
        if (warehouse is null) return null;

        return new(
            (Guid)warehouse.Id,
            warehouse.Name,
            warehouse.Inventory
                .Select(i =>
                    new InventoryItemDto((Guid)i.ItemId, (string)i.ItemInfo.Sku, (string)i.ItemInfo.Name, i.Quantity))
                .ToList());
    }
}

public class CreateWarehouseHandler(IWarehouseRepository repository)
{
    public async Task<WarehouseDto> Handle(CreateWarehouseCommand request, CancellationToken cancellationToken)
    {
        var warehouse = new Warehouse(request.Name);
        await repository.AddAsync(warehouse, cancellationToken);
        await repository.SaveChangesAsync(cancellationToken);

        return new((Guid)warehouse.Id, warehouse.Name);
    }
}

public class AddStockHandler(IWarehouseRepository warehouseRepository, IItemRepository itemRepository)
{
    public async Task Handle(AddStockCommand request, CancellationToken cancellationToken)
    {
        var warehouse = await warehouseRepository.GetByIdAsync((WarehouseId)request.WarehouseId, cancellationToken);
        if (warehouse is null) throw new("Warehouse not found");

        var item = await itemRepository.GetByIdAsync((ItemId)request.ItemId, cancellationToken);
        if (item is null) throw new("Item not found");

        warehouse.AddStock(item.Id, new(item.Sku, item.Name), request.Quantity);
        await warehouseRepository.SaveChangesAsync(cancellationToken);
    }
}

public class MoveStockHandler(IWarehouseRepository warehouseRepository, IItemRepository itemRepository)
{
    public async Task Handle(MoveStockCommand request, CancellationToken cancellationToken)
    {
        var sourceWarehouse =
            await warehouseRepository.GetByIdAsync((WarehouseId)request.SourceWarehouseId, cancellationToken);
        if (sourceWarehouse is null) throw new("Source warehouse not found");

        var destinationWarehouse =
            await warehouseRepository.GetByIdAsync((WarehouseId)request.DestinationWarehouseId, cancellationToken);
        if (destinationWarehouse is null) throw new("Destination warehouse not found");

        var item = await itemRepository.GetByIdAsync((ItemId)request.ItemId, cancellationToken);
        if (item is null) throw new("Item not found");

        sourceWarehouse.RemoveStock((ItemId)request.ItemId, request.Quantity);
        destinationWarehouse.AddStock((ItemId)request.ItemId, new(item.Sku, item.Name), request.Quantity);

        await warehouseRepository.SaveChangesAsync(cancellationToken);
    }
}

public static class WarehouseHandlerExtensions
{
    public static IServiceCollection AddWarehouseHandlers(this IServiceCollection services)
    {
        services.AddScoped<GetAllWarehousesHandler>();
        services.AddScoped<GetWarehouseByIdHandler>();
        services.AddScoped<CreateWarehouseHandler>();
        services.AddScoped<AddStockHandler>();
        services.AddScoped<MoveStockHandler>();

        return services;
    }
}