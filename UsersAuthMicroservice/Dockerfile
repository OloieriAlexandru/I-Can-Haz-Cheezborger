FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build

WORKDIR /app

COPY *.sln .
COPY BusinessLogic/*.csproj ./BusinessLogic/
COPY BusinessLogic.Tests/*.csproj ./BusinessLogic.Tests/
COPY Common/*.csproj ./Common/
COPY DataAccess/*.csproj ./DataAccess/
COPY Entities/*.csproj ./Entities/
COPY Models/*.csproj ./Models/
COPY Service/*.csproj ./Service/
COPY UsersAuthMicroservice.IntegrationTests ./UsersAuthMicroservice.IntegrationTests/

RUN dotnet restore

COPY BusinessLogic/* ./BusinessLogic/
COPY Common/* ./Common/
COPY DataAccess/* ./DataAccess/
COPY Entities/* ./Entities
COPY Models/* ./Models
COPY Service/* ./Service/

WORKDIR /app/Service
# Run Migrations
RUN chmod +x /app/Service/apply-migration.sh
RUN /app/Service/apply-migration.sh
# Build app
RUN dotnet publish -c Release -o service-build

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS runtime

WORKDIR /app

COPY --from=build /app/Service/service-build ./
ENTRYPOINT [ "dotnet", "Service.dll" ]
