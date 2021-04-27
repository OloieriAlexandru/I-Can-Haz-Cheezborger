dotnet sonarscanner begin /k:"UsersAuthMicroservice" /d:sonar.host.url="http://localhost:9000" /d:sonar.login="9f68ecf96e7cf3b0c2a2ca686b8e06eac2a30237"
dotnet build /t:Rebuild
dotnet sonarscanner end /d:sonar.login="9f68ecf96e7cf3b0c2a2ca686b8e06eac2a30237"
