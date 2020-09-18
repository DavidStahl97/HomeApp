#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["HomeCloud.Users/HomeCloud.Users.WebAPI/HomeCloud.Users.WebAPI.csproj", "HomeCloud.Users/HomeCloud.Users.WebAPI/"]
RUN dotnet restore "HomeCloud.Users/HomeCloud.Users.WebAPI/HomeCloud.Users.WebAPI.csproj"
COPY . .
WORKDIR "/src/HomeCloud.Users/HomeCloud.Users.WebAPI"
RUN dotnet build "HomeCloud.Users.WebAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "HomeCloud.Users.WebAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HomeCloud.Users.WebAPI.dll"]