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
        modelBuilder.Entity<Warehouse>(b =>
        {
            b.HasKey(w => w.Id);
            
            b.Property(w => w.Name)
                .IsRequired()
                .HasMaxLength(100);
            b.HasIndex(w => w.Name)
                .IsUnique();
            
            b.Property(w => w.Id)
                .ValueGeneratedOnAdd();
            b.Property(w => w.Id)
                .HasConversion<WarehouseIdValueConverter>();

            b.HasMany<StockItem>(nameof(Warehouse.Inventory))
                .WithOne()
                .HasForeignKey(s => s.WarehouseId);

            b.Metadata.FindNavigation(nameof(Warehouse.Inventory))!
                .SetPropertyAccessMode(PropertyAccessMode.Field);
        });

        modelBuilder.Entity<Item>(b =>
        {
            b.HasKey(i => i.Id);
            
            b.Property(i => i.Name)
                .IsRequired()
                .HasMaxLength(100);
            b.HasIndex(i => i.Name)
                .IsUnique();
            b.Property(i => i.Name)
                .HasConversion<ItemNameValueConverter>();

            b.Property(i => i.Sku)
                .IsRequired()
                .HasMaxLength(50);
            b.HasIndex(i => i.Sku)
                .IsUnique();
            b.Property(i => i.Sku)
                .HasConversion<ItemSkuValueConverter>();

            b.Property(i => i.Id)
                .ValueGeneratedOnAdd();
            b.Property(i => i.Id)
                .HasConversion<ItemIdValueConverter>();
        });

        modelBuilder.Entity<StockItem>(b =>
        {
            b.HasKey(si => si.Id);
            
            b.Property(si => si.Quantity)
                .IsRequired();
            
            b.HasIndex(si => new { si.WarehouseId, si.ItemId })
                .IsUnique();
            
            b.Property(si => si.Id)
                .ValueGeneratedOnAdd();
            b.Property(si => si.Id)
                .HasConversion<StockItemIdValueConverter>();
            
            b.Property(si => si.WarehouseId)
                .HasConversion<WarehouseIdValueConverter>();
            
            b.Property(si => si.ItemId)
                .HasConversion<ItemIdValueConverter>();

            b.OwnsOne(si => si.ItemInfo, ii =>
            {
                ii.Property(i => i.Sku)
                    .IsRequired()
                    .HasMaxLength(50);
                ii.Property(i => i.Sku)
                    .HasConversion<ItemSkuValueConverter>();

                ii.Property(i => i.Name)
                    .IsRequired()
                    .HasMaxLength(100);
                ii.Property(i => i.Name)
                    .HasConversion<ItemNameValueConverter>();
            });
        });
    }
}