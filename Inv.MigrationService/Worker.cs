using System.Diagnostics;
using Bogus;
using Inv.Domain.Entities;
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
        await strategy.ExecuteAsync(dbContext, async (context, cancellationToken) =>
        {
            // Run migration in a transaction to avoid partial migration if it fails.
            await context.Database.MigrateAsync(cancellationToken);
        }, cancellationToken);
    }

    private static async Task SeedDataAsync(WarehouseContext dbContext, CancellationToken cancellationToken)
    {
        var testItems = new Faker<Item>()
            .CustomInstantiator(f => new(f.Commerce.Ean13(), f.Commerce.ProductName()))
            .Generate(10);

        var testWarehouses = new Faker<Warehouse>()
            .CustomInstantiator(f => new(f.Commerce.Department()))
            .Generate(2);

        await dbContext.Items.AddRangeAsync(testItems, cancellationToken);
        await dbContext.Warehouses.AddRangeAsync(testWarehouses, cancellationToken);

        await dbContext.SaveChangesAsync(cancellationToken);
    }
}