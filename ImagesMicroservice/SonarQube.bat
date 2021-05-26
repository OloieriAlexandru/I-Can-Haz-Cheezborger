dotnet sonarscanner begin /k:"ImagesMicroservice" /d:sonar.host.url="http://localhost:9000" /d:sonar.login="00369b69cb945e2bb1be7a21ca44ff1fac6b89c2"
dotnet build /t:Rebuild
dotnet sonarscanner end /d:sonar.login="00369b69cb945e2bb1be7a21ca44ff1fac6b89c2"
