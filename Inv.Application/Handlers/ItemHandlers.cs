using Inv.Application.Interfaces;
using Inv.Application.Queries;
using Microsoft.Extensions.DependencyInjection;

namespace Inv.Application.Handlers;

public class GetAllItemsHandler(IItemRepository repository)
{
    public async Task<List<ItemDto>> Handle(GetAllItemsQuery request, CancellationToken cancellationToken)
    {
        return (await repository.GetAllAsync(cancellationToken))
            .Select(i => new ItemDto((Guid)i.Id, i.Name, i.Sku)).ToList();
    }
}

public static class ItemHandlerExtensions
{
    public static IServiceCollection AddItemHandlers(this IServiceCollection services)
    {
        services.AddScoped<GetAllItemsHandler>();

        return services;
    }
}