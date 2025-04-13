using Inv.API.Endpoints;
using Inv.Application;
using Inv.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi("v1", openApi =>
{
    openApi.AddDocumentTransformer((document, _, _) =>
    {
        document.Servers = [];

        return Task.CompletedTask;
    });
});

builder.AddServiceDefaults();

builder.Services.AddDbContext<WarehouseContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("whse"),
        x => x.MigrationsAssembly(typeof(WarehouseContext).Assembly));

    if (builder.Environment.IsDevelopment())
        options.LogTo(Console.WriteLine);
});

builder.Services.AddInfrastructure();
builder.Services.AddApplication();

builder.Services.AddCors(cors =>
{
    cors.AddDefaultPolicy(policy =>
    {
        if (builder.Environment.IsDevelopment())
            policy.AllowAnyHeader()
                .AllowAnyOrigin()
                .AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.MapDefaultEndpoints();

app.MapGroup("/warehouses")
    .MapWarehouse();

app.MapGroup("/items")
    .MapItem();

app.UseHttpsRedirection();

app.Run();