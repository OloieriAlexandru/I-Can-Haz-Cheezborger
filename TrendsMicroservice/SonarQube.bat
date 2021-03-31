dotnet sonarscanner begin /k:"TrendsMicroservice" /d:sonar.host.url="http://localhost:9000" /d:sonar.login="77a888356ac566514f99e3b8d8c5649be121b71c"
dotnet build /t:Rebuild
dotnet sonarscanner end /d:sonar.login="77a888356ac566514f99e3b8d8c5649be121b71c"
