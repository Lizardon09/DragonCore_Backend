FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base

WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build

WORKDIR /src
COPY ["./DragonCore.API/DragonCore.API.csproj", "DragonCore.API/"]
COPY ["./DragonCore.Domain/DragonCore.Domain.csproj", "DragonCore.Domain/"]
COPY ["./BasicHelpers.Infrastructure/BasicHelpers.Infrastructure.csproj", "BasicHelpers.Infrastructure/"]

RUN dotnet dev-certs https --clean
RUN dotnet dev-certs https -ep devcerts/DragonCore.API.pfx -p Croagunk1!
RUN dotnet dev-certs https --trust

RUN dotnet restore "./DragonCore.API/DragonCore.API.csproj"
COPY . .
WORKDIR "/src/DragonCore.API"
RUN dotnet build "DragonCore.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DragonCore.API.csproj" -c Release -o /app/publish

FROM base AS final
ENV ASPNETCORE_URLS="http://*:44314"
EXPOSE 44314
WORKDIR /app
COPY --from=publish /app/publish .
COPY --from=publish /src/devcerts/DragonCore.API.pfx ./certs/
ENTRYPOINT ["dotnet", "DragonCore.API.dll"]