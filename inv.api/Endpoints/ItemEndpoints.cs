using Inv.Application.Handlers;
using Inv.Application.Queries;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Inv.API.Endpoints;

public static class ItemEndpoints
{
    public static RouteGroupBuilder MapItem(this RouteGroupBuilder group)
    {
        group.MapGet("/", GetAllItemsAsync);

        return group;
    }

    private static async Task<Ok<List<ItemDto>>> GetAllItemsAsync([FromServices] GetAllItemsHandler handler,
        CancellationToken cancellationToken)
    {
        return TypedResults.Ok(await handler.Handle(new(), cancellationToken));
    }
}