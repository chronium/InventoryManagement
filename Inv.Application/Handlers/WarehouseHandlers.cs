using Inv.Application.Interfaces;
using Inv.Application.Queries;

namespace Inv.Application.Handlers;

public class GetAllWarehousesHandler(IWarehouseRepository repository)
{
    public async Task<List<WarehouseDto>> Handle(GetAllWarehousesQuery request, CancellationToken cancellationToken)
    {
        return (await repository.GetAllAsync(cancellationToken))
            .Select(w => new WarehouseDto((Guid)w.Id, w.Name)).ToList();
    }
}