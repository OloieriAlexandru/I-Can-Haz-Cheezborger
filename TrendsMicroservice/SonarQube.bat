dotnet sonarscanner begin ^
    /k:"TrendsMicroservice" ^
    /d:sonar.host.url="http://localhost:9000" ^
    /d:sonar.login="77a888356ac566514f99e3b8d8c5649be121b71c" ^
    /d:sonar.cs.opencover.reportsPaths=".\BusinessLogic.Tests\coverage.opencover.xml,.\Service.Tests\coverage.opencover.xml" ^
    /d:sonar.coverage.exclusions="**\DataAccess\Migrations\**,**\DataAccess\Configurations\**,**\DataAccess\Seed\**,**\ExtensionMethods\**,**\Entities\**,**\Models\**"
dotnet build /t:Rebuild                                              

cd ./BusinessLogic.Tests
dotnet test /p:CollectCoverage=true /p:CoverletOutputFormat=opencover /p:CoverletOutput=".\coverage.opencover.xml"
cd ..

cd ./Service.Tests
dotnet test /p:CollectCoverage=true /p:CoverletOutputFormat=opencover /p:CoverletOutput=".\coverage.opencover.xml"
cd ..

dotnet sonarscanner end /d:sonar.login="77a888356ac566514f99e3b8d8c5649be121b71c"
