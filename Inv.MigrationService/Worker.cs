using System.Diagnostics;
using Bogus;
using Inv.Domain.Entities;
using Inv.Domain.ValueObjects;
using Inv.Infrastructure;
using Microsoft.EntityFrameworkCore;
using OpenTelemetry.Trace;

namespace Inv.MigrationService;

public class Worker(
    IServiceProvider serviceProvider,
    IHostApplicationLifetime hostApplicationLifetime) : BackgroundService
{
    public const string ActivitySourceName = "Migrations";
    private static readonly ActivitySource s_activitySource = new(ActivitySourceName);

    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        using var activity = s_activitySource.StartActivity("Migrating database", ActivityKind.Client);

        try
        {
            using var scope = serviceProvider.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<WarehouseContext>();

            await RunMigrationAsync(dbContext, cancellationToken);
            await SeedDataAsync(dbContext, cancellationToken);
        }
        catch (Exception ex)
        {
            activity?.RecordException(ex);
            throw;
        }

        hostApplicationLifetime.StopApplication();
    }

    private static async Task RunMigrationAsync(WarehouseContext dbContext, CancellationToken cancellationToken)
    {
        var strategy = dbContext.Database.CreateExecutionStrategy();
        await strategy.ExecuteAsync(dbContext, static async (dbContext, cancellationToken) =>
        {
            // Run migration in a transaction to avoid partial migration if it fails.
            await dbContext.Database.MigrateAsync(cancellationToken);
        }, cancellationToken);
    }

    private static async Task SeedDataAsync(WarehouseContext dbContext, CancellationToken cancellationToken)
    {
        var randomizer = new Randomizer();

        var testItems = new Faker<Item>()
            .CustomInstantiator(f => new((ItemSku)f.Commerce.Ean13(), (ItemName)f.Commerce.ProductName()))
            .Generate(10);
        await dbContext.Items.AddRangeAsync(testItems, cancellationToken);

        var testWarehouses = new Faker<Warehouse>()
            .CustomInstantiator(f => new(f.Commerce.Department()))
            .Generate(2);
        await dbContext.Warehouses.AddRangeAsync(testWarehouses, cancellationToken);

        foreach (var warehouse in testWarehouses)
        foreach (var item in randomizer.ListItems(testItems))
            warehouse.AddStock(item.Id, new(item.Sku, item.Name), new Random().Next(1, 100));

        await dbContext.SaveChangesAsync(cancellationToken);
    }
}