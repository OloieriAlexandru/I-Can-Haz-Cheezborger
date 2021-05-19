FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build

WORKDIR /app

COPY *.sln .
COPY BusinessLogic/*.csproj ./BusinessLogic/
COPY BusinessLogic.Tests/*.csproj ./BusinessLogic.Tests/
COPY Common.Constraints/*.csproj ./Common.Constraints/
COPY DataAccess/*.csproj ./DataAccess/
COPY Entities/*.csproj ./Entities/
COPY Models/*.csproj ./Models/
COPY Service/*.csproj ./Service/
COPY Service.Tests/*.csproj ./Service.Tests/
COPY TrendsMicroservice.IntegrationTests ./TrendsMicroservice.IntegrationTests/

RUN dotnet restore

COPY BusinessLogic/* ./BusinessLogic/
COPY Common.Constraints/* ./Common.Constraints/
COPY DataAccess/* ./DataAccess/
COPY Entities/* ./Entities/
COPY Models/* ./Models/
COPY Service/* ./Service/

WORKDIR /app/Service
# Run Migrations
RUN chmod +x /app/Service/apply-migration.sh
RUN /app/Service/apply-migration.sh
# Build app
RUN dotnet publish -c Release -o service-build

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS runtime

WORKDIR /app

RUN sudo apt update && sudo apt install -y libc-dev
ENV ASPNETCORE_URLS=http://*:${PORT}
ENV ASPNETCORE_ENVIRONMENT=Production
ENV TAPTAKE_SEED=False
EXPOSE $PORT
EXPOSE 80
EXPOSE 443
COPY --from=build /app/Service/service-build ./
ENTRYPOINT [ "dotnet", "Service.dll" ]
