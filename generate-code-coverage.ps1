dotnet test test/backend/HomeCloud.Maps.Server.UnitTests/HomeCloud.Maps.Server.UnitTests.csproj /p:CollectCoverage=true  /p:CoverletOutput=../../../CoverageResults/
dotnet test test/backend/HomeCloud.Maps.Infrastructure.UnitTests/HomeCloud.Maps.Infrastructure.UnitTests.csproj /p:CollectCoverage=true  /p:CoverletOutput=../../../CoverageResults/ /p:MergeWith="../../../CoverageResults/coverage.json" /p:CoverletOutputFormat=cobertura

reportgenerator "-reports:CoverageResults/coverage.cobertura.xml" "-targetdir:coveragereport" -reporttypes:Html