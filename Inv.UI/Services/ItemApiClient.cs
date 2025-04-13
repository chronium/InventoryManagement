using Inv.Application.Shared.CommandDtos;
using Inv.Application.Shared.QueryDtos;

namespace Inv.UI.Services;

public class ItemApiClient(HttpClient http)
{
    public async Task<List<ItemDto>> GetAllItemsAsync(CancellationToken cancellationToken = default)
    {
        var response = await http.GetAsync("items", cancellationToken);
        response.EnsureSuccessStatusCode();
        return (await response.Content.ReadFromJsonAsync<List<ItemDto>>(cancellationToken))!;
    }

    public async Task CreateItem(CreateItemDto item, CancellationToken cancellationToken = default)
    {
        var response = await http.PostAsJsonAsync("items", item, cancellationToken);
        response.EnsureSuccessStatusCode();
    }
}