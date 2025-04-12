using Inv.Domain.Entities;
using Inv.Infrastructure.ValueConverters;
using Microsoft.EntityFrameworkCore;

namespace Inv.Infrastructure;

public class WarehouseContext(DbContextOptions<WarehouseContext> options) : DbContext(options)
{
    public DbSet<Warehouse> Warehouses { get; set; }
    public DbSet<Item> Items { get; set; }
    public DbSet<StockItem> StockItems { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var warehouseEntity = modelBuilder.Entity<Warehouse>();

        warehouseEntity.HasKey(w => w.Id);
        warehouseEntity.Property(w => w.Name)
            .IsRequired()
            .HasMaxLength(100);
        warehouseEntity.HasIndex(w => w.Name)
            .IsUnique();
        warehouseEntity.Property(w => w.Id)
            .ValueGeneratedOnAdd();
        warehouseEntity.Property(w => w.Id)
            .HasConversion<WarehouseIdValueConverter>();

        var itemEntity = modelBuilder.Entity<Item>();

        itemEntity.HasKey(i => i.Id);
        itemEntity.Property(i => i.Name)
            .IsRequired()
            .HasMaxLength(100);
        itemEntity.HasIndex(i => i.Name)
            .IsUnique();
        itemEntity.Property(i => i.Sku)
            .IsRequired()
            .HasMaxLength(50);
        itemEntity.HasIndex(i => i.Sku)
            .IsUnique();
        itemEntity.Property(i => i.Id)
            .ValueGeneratedOnAdd();
        itemEntity.Property(i => i.Id)
            .HasConversion<ItemIdValueConverter>();

        var stockItemEntity = modelBuilder.Entity<StockItem>();

        stockItemEntity.HasKey(si => si.Id);
        stockItemEntity.Property(si => si.Quantity)
            .IsRequired();
        stockItemEntity.HasIndex(si => new { si.WarehouseId, si.ItemId })
            .IsUnique();
        stockItemEntity.Property(si => si.Id)
            .ValueGeneratedOnAdd();
        stockItemEntity.Property(si => si.Id)
            .HasConversion<StockItemIdValueConverter>();
        stockItemEntity.Property(si => si.WarehouseId)
            .HasConversion<WarehouseIdValueConverter>();
        stockItemEntity.Property(si => si.ItemId)
            .HasConversion<ItemIdValueConverter>();
    }
}