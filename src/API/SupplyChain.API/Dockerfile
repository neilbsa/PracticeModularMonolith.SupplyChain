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
COPY ["src/API/SupplyChain.API/SupplyChain.API.csproj", "src/API/SupplyChain.API/"]
COPY ["src/Modules/Users/SupplyChain.Modules.Users.Infrastructure/SupplyChain.Modules.Users.Infrastructure.csproj", "src/Modules/Users/SupplyChain.Modules.Users.Infrastructure/"]
COPY ["src/Common/SupplyChain.Common.Infrastructure/SupplyChain.Common.Infrastructure.csproj", "src/Common/SupplyChain.Common.Infrastructure/"]
COPY ["src/Common/SupplyChain.Common.Application/SupplyChain.Common.Application.csproj", "src/Common/SupplyChain.Common.Application/"]
COPY ["src/Common/SupplyChain.Common.Domain/SupplyChain.Common.Domain.csproj", "src/Common/SupplyChain.Common.Domain/"]
COPY ["src/Modules/Users/SupplyChain.Modules.Users.Application/SupplyChain.Modules.Users.Application.csproj", "src/Modules/Users/SupplyChain.Modules.Users.Application/"]
COPY ["src/Modules/Users/SupplyChain.Modules.Users.Domain/SupplyChain.Modules.Users.Domain.csproj", "src/Modules/Users/SupplyChain.Modules.Users.Domain/"]
COPY ["src/Modules/Users/SupplyChain.Modules.Users.Presentation/SupplyChain.Modules.Users.Presentation.csproj", "src/Modules/Users/SupplyChain.Modules.Users.Presentation/"]
COPY ["src/Common/SupplyChain.Common.Presentation/SupplyChain.Common.Presentation.csproj", "src/Common/SupplyChain.Common.Presentation/"]
COPY ["src/Modules/Warehouses/SupplyChain.Modules.Warehouses.Infrastructure/SupplyChain.Modules.Warehouses.Infrastructure.csproj", "src/Modules/Warehouses/SupplyChain.Modules.Warehouses.Infrastructure/"]
COPY ["src/Modules/Warehouses/SupplyChain.Modules.Warehouses.Application/SupplyChain.Modules.Warehouses.Application.csproj", "src/Modules/Warehouses/SupplyChain.Modules.Warehouses.Application/"]
COPY ["src/Modules/Warehouses/SupplyChain.Modules.Warehouses.Domain/SupplyChain.Modules.Warehouses.Domain.csproj", "src/Modules/Warehouses/SupplyChain.Modules.Warehouses.Domain/"]
COPY ["src/Modules/Warehouses/SupplyChain.Modules.Warehouses.Presentation/SupplyChain.Modules.Warehouses.Presentation.csproj", "src/Modules/Warehouses/SupplyChain.Modules.Warehouses.Presentation/"]
RUN dotnet restore "./src/API/SupplyChain.API/SupplyChain.API.csproj"
COPY . .
WORKDIR "/src/src/API/SupplyChain.API"
RUN dotnet build "./SupplyChain.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./SupplyChain.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SupplyChain.API.dll"]