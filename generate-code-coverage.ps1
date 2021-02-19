dotnet test HomeCloud.sln /p:CollectCoverage=true  /p:CoverletOutput=./code-coverage/ /p:CoverletOutputFormat=cobertura

reportgenerator "-reports:./test/HomeCloud.Maps.UnitTests/code-coverage/coverage.cobertura.xml" "-targetdir:./test/HomeCloud.Maps.UnitTests/code-coverage/coveragereport" -reporttypes:Html