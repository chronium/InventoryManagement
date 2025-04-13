using Inv.Application.Interfaces;
using Inv.Application.Queries;

namespace Inv.Application.Handlers;

public class GetAllItemsHandler(IItemRepository repository)
{
    public async Task<List<ItemDto>> Handle(GetAllItemsQuery request, CancellationToken cancellationToken)
    {
        return (await repository.GetAllAsync(cancellationToken))
            .Select(i => new ItemDto((Guid)i.Id, i.Name, i.Sku)).ToList();
    }
}