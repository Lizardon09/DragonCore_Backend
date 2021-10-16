FROM mcr.microsoft.com/dotnet/core/aspnet AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk AS build
WORKDIR /src
COPY ["./", "./"]
RUN dotnet restore "./DragonCore.API/DragonCore.API.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "./DragonCore.API/DragonCore.API.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "./DragonCore.API/DragonCore.API.csproj" -c Release -o /app

FROM base AS final
ENV ASPNETCORE_URLS="http://*:44314"
EXPOSE 44314
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT [ "dotnet", "DragonCore.API.dll" ]

## Instructions

# 1. Run docker build -t dragoncore -f dev.dockerfile .
# 2. Run docker run -d --name dragoncore-api -p 44314:44314 dragoncore