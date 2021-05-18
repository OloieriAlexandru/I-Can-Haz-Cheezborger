#!/bin/bash

export ASPNETCORE_ENVIRONMENT="Production"
export PATH="$PATH:/root/.dotnet/tools"
dotnet tool install --global dotnet-ef
mv appsettings.Development.json appsettings.json
dotnet ef database update
