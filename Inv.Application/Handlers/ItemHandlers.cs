using Inv.Application.Commands;
using Inv.Application.Interfaces;
using Inv.Application.Queries;
using Inv.Domain.Entities;
using Microsoft.Extensions.DependencyInjection;

namespace Inv.Application.Handlers;

public class GetAllItemsHandler(IItemRepository repository)
{
    public async Task<List<ItemDto>> Handle(GetAllItemsQuery request, CancellationToken cancellationToken)
    {
        return (await repository.GetAllAsync(cancellationToken))
            .Select(i => new ItemDto((Guid)i.Id, (string)i.Name, (string)i.Sku)).ToList();
    }
}

public class CreateItemHandler(IItemRepository repository)
{
    public async Task<ItemDto> Handle(CreateItemCommand request, CancellationToken cancellationToken)
    {
        var item = new Item(request.Sku, request.Name);
        await repository.AddAsync(item, cancellationToken);
        await repository.SaveChangesAsync(cancellationToken);

        return new((Guid)item.Id, (string)item.Name, (string)item.Sku);
    }
}

public static class ItemHandlerExtensions
{
    public static IServiceCollection AddItemHandlers(this IServiceCollection services)
    {
        services.AddScoped<GetAllItemsHandler>();
        services.AddScoped<CreateItemHandler>();

        return services;
    }
}