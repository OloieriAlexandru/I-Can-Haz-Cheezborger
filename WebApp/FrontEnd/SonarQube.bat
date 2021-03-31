dotnet sonarscanner begin /k:"ICanHazCheezborgerWebApp" /d:sonar.host.url="http://localhost:9000" /d:sonar.login="3789c10eae026549dd1b74cfa1a99b3609134bde"
dotnet build /t:Rebuild
dotnet sonarscanner end /d:sonar.login="3789c10eae026549dd1b74cfa1a99b3609134bde"
