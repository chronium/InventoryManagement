using Inv.Application.Handlers;
using Inv.Application.Shared.CommandDtos;
using Inv.Application.Shared.QueryDtos;
using Inv.Domain.ValueObjects;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Inv.API.Endpoints;

public static class ItemEndpoints
{
    public static RouteGroupBuilder MapItem(this RouteGroupBuilder group)
    {
        group.MapGet("/", GetAllItemsAsync).WithName(nameof(GetAllItemsAsync));
        group.MapPost("/", CreateItemAsync).WithName(nameof(CreateItemAsync));

        return group;
    }

    private static async Task<Ok<List<ItemDto>>> GetAllItemsAsync([FromServices] GetAllItemsHandler handler,
        CancellationToken cancellationToken)
    {
        return TypedResults.Ok(await handler.Handle(new(), cancellationToken));
    }

    private static async Task<CreatedAtRoute<ItemDto>> CreateItemAsync(
        [FromServices] CreateItemHandler handler,
        [FromBody] CreateItemDto command,
        CancellationToken cancellationToken)
    {
        var result = await handler.Handle(new((ItemSku)command.Sku, (ItemName)command.Name), cancellationToken);
        return TypedResults.CreatedAtRoute(result, nameof(GetAllItemsAsync), new { id = result.Id });
    }
}