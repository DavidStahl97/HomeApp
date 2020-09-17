#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
ENV ASPNETCORE_URLS=http://+:5001;https://+:5000
WORKDIR /app
EXPOSE 5001
EXPOSE 5000

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["HomeCloud.Gateway/HomeCloud.Gateway.csproj", "HomeCloud.Gateway/"]
COPY ["HomeCloud.Home/HomeCloud.Home.Client/HomeCloud.Home.Client.csproj", "HomeCloud.Home/HomeCloud.Home.Client/"]
COPY ["HomeCloud.Book/HomeCloud.Book.Client/HomeCloud.Book.Client.csproj", "HomeCloud.Book/HomeCloud.Book.Client/"]
COPY ["HomeCloud.Client.Common/HomeCloud.Client.Common.csproj", "HomeCloud.Client.Common/"]
COPY ["HomeCloud.Map/HomeCloud.Map.Client/HomeCloud.Map.Client.csproj", "HomeCloud.Map/HomeCloud.Map.Client/"]
COPY ["HomeCloud.Map/HomeCloud.Map.Shared/HomeCloud.Map.Shared.csproj", "HomeCloud.Map/HomeCloud.Map.Shared/"]
RUN dotnet restore "HomeCloud.Gateway/HomeCloud.Gateway.csproj"
COPY . .
WORKDIR "/src/HomeCloud.Gateway"
RUN dotnet build "HomeCloud.Gateway.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "HomeCloud.Gateway.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HomeCloud.Gateway.dll"]
