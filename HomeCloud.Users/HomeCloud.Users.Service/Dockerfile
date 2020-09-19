#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 64676

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["HomeCloud.Users/HomeCloud.Users.Service/HomeCloud.User.Server.csproj", "HomeCloud.Users/HomeCloud.Users.Service/"]
RUN dotnet restore "HomeCloud.Users/HomeCloud.Users.Service/HomeCloud.User.Server.csproj"
COPY . .
WORKDIR "/src/HomeCloud.Users/HomeCloud.Users.Service"
RUN dotnet build "HomeCloud.User.Server.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "HomeCloud.User.Server.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HomeCloud.User.Server.dll"]