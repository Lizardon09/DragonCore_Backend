#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base

WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build

WORKDIR /src
COPY ["DragonCore.API/DragonCore.API.csproj", "DragonCore.API/"]
COPY ["ElasticSearch.Infrastructure/ElasticSearch.Infrastructure.csproj", "ElasticSearch.Infrastructure/"]
COPY ["ElasticSearch.Domain/ElasticSearch.Domain.csproj", "ElasticSearch.Domain/"]
COPY ["DragonCore.Domain/DragonCore.Domain.csproj", "DragonCore.Domain/"]

RUN dotnet dev-certs https --clean
RUN dotnet dev-certs https -ep devcerts/DragonCore.API.pfx -p Croagunk1!
RUN dotnet dev-certs https --trust

RUN dotnet restore "DragonCore.API/DragonCore.API.csproj"
COPY . .
WORKDIR "/src/DragonCore.API"
RUN dotnet build "DragonCore.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DragonCore.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
COPY --from=publish /src/devcerts/DragonCore.API.pfx ./certs/
ENTRYPOINT ["dotnet", "DragonCore.API.dll"]