FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS runtime

WORKDIR /app

ENV ASPNETCORE_URLS=http://*:${PORT}
ENV ASPNETCORE_ENVIRONMENT=Production
ENV TAPTAKE_SEED=False
EXPOSE $PORT
EXPOSE 80
EXPOSE 443
COPY ./web-app-build ./
ENTRYPOINT [ "dotnet", "TrendsViewer.dll" ]
