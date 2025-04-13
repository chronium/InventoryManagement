using Inv.Domain.ValueObjects;

namespace Inv.Application.Commands;

public record CreateItemCommand(ItemSku Sku, ItemName Name);