using Projects;

var builder = DistributedApplication.CreateBuilder(args);

var postgres = builder.AddPostgres("pgserv");
var db = postgres.AddDatabase("whse");

var migrationService = builder.AddProject<Inv_MigrationService>("migration")
    .WithReference(db)
    .WaitFor(db);

var api = builder.AddProject<Inv_API>("api")
    .WithReference(db)
    .WaitFor(db)
    .WaitForCompletion(migrationService);

var ui = builder.AddProject<Inv_UI>("ui")
    .WithReference(api)
    .WaitFor(api);

builder.Build().Run();