using Inv.Infrastructure;
using Inv.MigrationService;

var builder = Host.CreateApplicationBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddHostedService<Worker>();

builder.Services.AddOpenTelemetry()
    .WithTracing(tracing => tracing.AddSource(Worker.ActivitySourceName));

builder.AddNpgsqlDbContext<WarehouseContext>("whse", configureDbContextOptions: options =>
{
    if (builder.Environment.IsDevelopment())
        options.LogTo(Console.WriteLine);
});

var host = builder.Build();
host.Run();