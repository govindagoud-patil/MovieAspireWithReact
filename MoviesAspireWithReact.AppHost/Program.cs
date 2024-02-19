var builder = DistributedApplication.CreateBuilder(args);

var webapi = builder.AddProject<Projects.Movies_webapi>("webapi");

var webapp = builder.AddNpmApp("react", "../webapp/react/movies")
                    .WithReference(webapi)
                    .WithEndpoint(containerPort: 3000, scheme: "http", env: "PORT")
                    .AsDockerfileInManifest();

builder.Build().Run();

