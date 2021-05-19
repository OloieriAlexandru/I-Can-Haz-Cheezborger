FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build

WORKDIR /app

COPY *.sln .
COPY BusinessLogic/*.csproj ./BusinessLogic/
COPY Common/*.csproj ./Common/
COPY DataAccess/*.csproj ./DataAccess/
COPY Entities/*.csproj ./Entities/
COPY Models/*.csproj ./Models/
COPY Service/*.csproj ./Service/

RUN dotnet restore

COPY BusinessLogic/* ./BusinessLogic/
COPY Common/* ./Common/
COPY DataAccess/* ./DataAccess/
COPY Entities/* ./Entities/
COPY Models/* ./Models/
COPY Service/* ./Service/

WORKDIR /app/Service
# Build app
RUN dotnet publish -c Release -o service-build

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS runtime

WORKDIR /app

ENV ASPNETCORE_URLS=http://*:${PORT}
ENV ASPNETCORE_ENVIRONMENT=Production
ENV TAPTAKE_SEED=False
EXPOSE $PORT
EXPOSE 80
EXPOSE 443
COPY --from=build /app/Service/service-build ./
ENTRYPOINT [ "dotnet", "Service.dll" ]
