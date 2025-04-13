using Inv.Application.Commands;
using Inv.Application.Handlers;
using Inv.Application.Queries;
using Inv.Application.Shared.CommandDtos;
using Inv.Application.Shared.QueryDtos;
using Inv.Domain.Entities.IdTypes;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Inv.API.Endpoints;

public static class WarehouseEndpoints
{
    public static RouteGroupBuilder MapWarehouse(this RouteGroupBuilder group)
    {
        group.MapGet("/", GetAllWarehousesAsync).WithName(nameof(GetAllWarehousesAsync));
        group.MapGet("/{id:guid}", GetWarehouseByIdAsync).WithName(nameof(GetWarehouseByIdAsync));
        group.MapPost("/", CreateWarehouseAsync).WithName(nameof(CreateWarehouseAsync));
        group.MapPost("/{id:guid}/stock", AddStockAsync).WithName(nameof(AddStockAsync));
        group.MapPost("/{id:guid}/move-stock", MoveStockAsync).WithName(nameof(MoveStockAsync));

        return group;
    }

    private static async Task<Ok<List<WarehouseDto>>> GetAllWarehousesAsync(
        [FromServices] GetAllWarehousesHandler handler,
        CancellationToken cancellationToken)
    {
        return TypedResults.Ok(await handler.Handle(new(), cancellationToken));
    }

    private static async Task<Results<Ok<WarehouseWithInventoryDto>, NotFound>> GetWarehouseByIdAsync(
        [FromServices] GetWarehouseByIdHandler handler,
        [FromRoute] Guid id,
        CancellationToken cancellationToken)
    {
        var result = await handler.Handle(new((WarehouseId)id), cancellationToken);
        return result is null ? TypedResults.NotFound() : TypedResults.Ok(result);
    }

    private static async Task<CreatedAtRoute<WarehouseDto>> CreateWarehouseAsync(
        [FromServices] CreateWarehouseHandler handler,
        [FromBody] CreateWarehouseDto command,
        CancellationToken cancellationToken)
    {
        var result = await handler.Handle(new(command.Name), cancellationToken);
        return TypedResults.CreatedAtRoute(result, nameof(GetWarehouseByIdAsync), new { id = result.Id });
    }
    
    private static async Task<NoContent> AddStockAsync(
        [FromServices] AddStockHandler handler,
        [FromRoute] Guid id,
        [FromBody] AddStockDto command,
        CancellationToken cancellationToken)
    {
        await handler.Handle(new((WarehouseId)id, (ItemId)command.ItemId, command.Quantity), cancellationToken);
        return TypedResults.NoContent();
    }
    
    private static async Task<NoContent> MoveStockAsync(
        [FromServices] MoveStockHandler handler,
        [FromRoute] Guid id,
        [FromBody] MoveStockDto command,
        CancellationToken cancellationToken)
    {
        await handler.Handle(new((WarehouseId)id, (WarehouseId)command.DestinationWarehouseId, (ItemId)command.ItemId, command.Quantity), cancellationToken);
        return TypedResults.NoContent();
    }
}