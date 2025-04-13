using Inv.Application.Handlers;
using Inv.Application.Queries;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Inv.API.Endpoints;

public static class WarehouseEndpoints
{
    public static RouteGroupBuilder MapWarehouse(this RouteGroupBuilder group)
    {
        group.MapGet("/", GetAllWarehousesAsync);
        group.MapGet("/{id:guid}", GetWarehouseByIdAsync);
        
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
        var result = await handler.Handle(new(id), cancellationToken);
        return result is null ? TypedResults.NotFound() : TypedResults.Ok(result);
    }
}