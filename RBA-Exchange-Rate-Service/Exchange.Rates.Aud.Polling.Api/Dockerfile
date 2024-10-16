# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Exchange.Rates.Aud.Polling.Api/Exchange.Rates.Aud.Polling.Api.csproj", "Exchange.Rates.Aud.Polling.Api/"]
COPY ["Exchange.Rates.RBA.Contracts/Exchange.Rates.RBA.Contracts.csproj", "Exchange.Rates.RBA.Contracts/"]
COPY ["Exchange.Rates.RBA.Core/Exchange.Rates.RBA.Core.csproj", "Exchange.Rates.RBA.Core/"]
RUN dotnet restore "./Exchange.Rates.Aud.Polling.Api/Exchange.Rates.Aud.Polling.Api.csproj"
COPY . .
WORKDIR "/src/Exchange.Rates.Aud.Polling.Api"
RUN dotnet build "./Exchange.Rates.Aud.Polling.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Exchange.Rates.Aud.Polling.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Exchange.Rates.Aud.Polling.Api.dll"]