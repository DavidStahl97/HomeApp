#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
ENV ASPNETCORE_URLS=http://+:5010
WORKDIR /app
EXPOSE 5010

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["HomeCloud.Map/HomeCloud.Map.WebAPI/HomeCloud.Map.Presentation/HomeCloud.Map.WebAPI.csproj", "HomeCloud.Map/HomeCloud.Map.WebAPI/HomeCloud.Map.Presentation/"]
COPY ["HomeCloud.Map/HomeCloud.Map.WebAPI/HomeCloud.Map.Infrastructure/HomeCloud.Map.Infrastructure.csproj", "HomeCloud.Map/HomeCloud.Map.WebAPI/HomeCloud.Map.Infrastructure/"]
COPY ["HomeCloud.Map/HomeCloud.Map.WebAPI/HomeCloud.Map.Application/HomeCloud.Map.Application.csproj", "HomeCloud.Map/HomeCloud.Map.WebAPI/HomeCloud.Map.Application/"]
COPY ["HomeCloud.Map/HomeCloud.Map.WebAPI/HomeCloud.Map.Domain/HomeCloud.Map.Domain.csproj", "HomeCloud.Map/HomeCloud.Map.WebAPI/HomeCloud.Map.Domain/"]
COPY ["HomeCloud.Map/HomeCloud.Map.Shared/HomeCloud.Map.Shared.csproj", "HomeCloud.Map/HomeCloud.Map.Shared/"]
RUN dotnet restore "HomeCloud.Map/HomeCloud.Map.WebAPI/HomeCloud.Map.Presentation/HomeCloud.Map.WebAPI.csproj"
COPY . .
WORKDIR "/src/HomeCloud.Map/HomeCloud.Map.WebAPI/HomeCloud.Map.Presentation"
RUN dotnet build "HomeCloud.Map.WebAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "HomeCloud.Map.WebAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HomeCloud.Map.WebAPI.dll"]
