#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-nanoserver-1903 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-nanoserver-1903 AS build
WORKDIR /src
COPY ["HomeApp/Server/Presentation/Server/HomeApp.Server.csproj", "HomeApp/Server/Presentation/Server/"]
COPY ["HomeApp/Server/Infrastructure/HomeApp.Infrastructure/HomeApp.Infrastructure.csproj", "HomeApp/Server/Infrastructure/HomeApp.Infrastructure/"]
COPY ["HomeApp/Server/Application/HomeApp.Application/HomeApp.Application.csproj", "HomeApp/Server/Application/HomeApp.Application/"]
COPY ["HomeApp/Server/Domain/HomeApp.Domain/HomeApp.Domain.csproj", "HomeApp/Server/Domain/HomeApp.Domain/"]
COPY ["HomeApp/Shared/HomeApp.Shared.csproj", "HomeApp/Shared/"]
COPY ["HomeApp/Client/Client/HomeApp.Client.csproj", "HomeApp/Client/Client/"]
RUN dotnet restore "HomeApp/Server/Presentation/Server/HomeApp.Server.csproj"
COPY . .
WORKDIR "/src/HomeApp/Server/Presentation/Server"
RUN dotnet build "HomeApp.Server.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "HomeApp.Server.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HomeApp.Server.dll"]