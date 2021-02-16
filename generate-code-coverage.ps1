dotnet test test/backend/HomeCloud.Maps.Server.UnitTests/HomeCloud.Maps.Server.UnitTests.csproj /p:CollectCoverage=true /p:CoverletOutputFormat=cobertura

reportgenerator "-reports:test/backend/HomeCloud.Maps.Server.UnitTests/coverage.cobertura.xml" "-targetdir:coveragereport" -reporttypes:Html