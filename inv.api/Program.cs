using Inv.Infrastructure;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.AddServiceDefaults();

builder.Services.AddDbContext<WarehouseContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("whse"),
        x => x.MigrationsAssembly(typeof(WarehouseContext).Assembly));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) app.MapOpenApi();

app.MapDefaultEndpoints();

app.UseHttpsRedirection();

app.Run();