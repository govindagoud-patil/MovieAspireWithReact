var builder = DistributedApplication.CreateBuilder(args);

var webapi = builder.AddProject<Projects.Movies_webapi>("webapi");

var webappBlazor = builder.AddProject<Projects.MovieBlazor>("webappBlazor")
                          .WithReference(webapi);
                       

var webapp = builder.AddNpmApp("webapp", "../webapp/react/movies")
                    .WithReference(webapi)
                    .WithEndpoint(containerPort: 3000, scheme: "http", env: "PORT")
                    .AsDockerfileInManifest();

builder.Build().Run();

