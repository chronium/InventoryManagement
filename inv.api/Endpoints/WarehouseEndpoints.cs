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
        
        return group;
    }
    
    public static async Task<Ok<List<WarehouseDto>>> GetAllWarehousesAsync(
        [FromServices] GetAllWarehousesHandler handler,
        CancellationToken cancellationToken)
    {
        return TypedResults.Ok(await handler.Handle(new(), cancellationToken));
    }
}