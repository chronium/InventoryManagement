var builder = DistributedApplication.CreateBuilder(args);

var postgres = builder.AddPostgres("pgserv");
var db = postgres.AddDatabase("whse");

var migrationService = builder.AddProject<Projects.Inv_MigrationService>("migration")
    .WithReference(db)
    .WaitFor(db);

var api = builder.AddProject<Projects.Inv_API>("api")
    .WithReference(db)
    .WaitFor(db)
    .WaitFor(migrationService);

builder.Build().Run();