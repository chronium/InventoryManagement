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
}