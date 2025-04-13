using Inv.Application.Shared.CommandDtos;
using Inv.Application.Shared.QueryDtos;

namespace Inv.UI.Services;

public class WarehouseApiClient(HttpClient http)
{
    public async Task<List<WarehouseDto>> GetAllWarehousesAsync(CancellationToken cancellationToken = default)
    {
        var response = await http.GetAsync("warehouses", cancellationToken);
        response.EnsureSuccessStatusCode();
        return (await response.Content.ReadFromJsonAsync<List<WarehouseDto>>(cancellationToken))!;
    }

    public async Task<WarehouseWithInventoryDto> GetWarehouseAsync(Guid id,
        CancellationToken cancellationToken = default)
    {
        var response = await http.GetAsync($"warehouses/{id}", cancellationToken);
        response.EnsureSuccessStatusCode();
        return (await response.Content.ReadFromJsonAsync<WarehouseWithInventoryDto>(cancellationToken))!;
    }

    public async Task CreateWarehouse(CreateWarehouseDto warehouse,
        CancellationToken cancellationToken = default)
    {
        var response = await http.PostAsJsonAsync("warehouses", warehouse, cancellationToken);
        response.EnsureSuccessStatusCode();
    }

    public async Task AddStock(Guid id, AddStockDto stock,
        CancellationToken cancellationToken = default)
    {
        var response = await http.PostAsJsonAsync($"warehouses/{id}/stock", stock, cancellationToken);
        response.EnsureSuccessStatusCode();
    }

    public async Task MoveStock(Guid id, MoveStockDto stock,
        CancellationToken cancellationToken = default)
    {
        var response = await http.PostAsJsonAsync($"warehouses/{id}/move-stock", stock, cancellationToken);
        response.EnsureSuccessStatusCode();
    }
}