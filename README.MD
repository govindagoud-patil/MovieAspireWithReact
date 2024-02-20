# Movie Project

![alt text](./MovieProject.png)

# How to Run the project in local

1. Download git clone https://github.com/govindagoud-patil/MovieAspireWithReact.git
2. Navigate to webapp and restore deps (npm install)
3. Install docker desktop if not installed in you local machine
4. Navigate to MoviesAspireWithReact.AppHost and run "dotnet run" . that is it your app is up and running,It lauches aspire dashboard ![](./AspireDashBoard.png)
5. Navigate to React . It asks for login it is optinal just ignore and hit login button will take you to the movie list page where you can do CRUD opeartion on movie list

# Aspire Manifest

1.Aspire project can generate a manifest that can later be used by other tools to
deploy into an environment. Think of it as the IL for a Distrubuted .Net app

dotnet run --project MoviesAspireWithReact.AppHost.csproj --publisher manifest --output-path aspire-manifest.json

2. we can use azd or Aspir8 to deploy or provision environment ![](./aspireAZD.png)

# deploy the manifest to k8s or compose with aspir8

1 .Install aspireate
dotnet tool install -g aspirate --prerelease 
2. Deploy the manifest to K8s or Compose with Aspir8
Kustomize : aspirate generate
compose: aspirate generate --output-format compose
