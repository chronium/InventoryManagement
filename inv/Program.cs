var builder = DistributedApplication.CreateBuilder(args);

var postgres = builder.AddPostgres("pgserv");
var db = postgres.AddDatabase("whse");

var api = builder.AddProject<Projects.Inv_API>("api")
    .WaitFor(db);

builder.Build().Run();