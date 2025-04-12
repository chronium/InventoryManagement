using Inv.Infrastructure;
using Inv.MigrationService;
using Microsoft.EntityFrameworkCore;

var builder = Host.CreateApplicationBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddHostedService<Worker>();

builder.Services.AddOpenTelemetry()
    .WithTracing(tracing => tracing.AddSource(Worker.ActivitySourceName));

builder.AddNpgsqlDbContext<WarehouseContext>("whse");

var host = builder.Build();
host.Run();