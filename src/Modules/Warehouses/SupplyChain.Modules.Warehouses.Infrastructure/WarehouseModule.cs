
using Evently.Common.Infrastructure.Interceptors;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SupplyChain.Common.Presentation.Endpoint;
using SupplyChain.Modules.Orders.IntegrationEvents;
using SupplyChain.Modules.Warehouses.Application.Abstractions.Data;
using SupplyChain.Modules.Warehouses.Domain.BinLocations.Repository;
using SupplyChain.Modules.Warehouses.Domain.CatalogQuantities.Repository;
using SupplyChain.Modules.Warehouses.Domain.Catalogs.Repository;
using SupplyChain.Modules.Warehouses.Domain.Warehouses.Repository;
using SupplyChain.Modules.Warehouses.Infrastructure.BinLocations;
using SupplyChain.Modules.Warehouses.Infrastructure.CatalogQuantities;
using SupplyChain.Modules.Warehouses.Infrastructure.Catalogs;
using SupplyChain.Modules.Warehouses.Infrastructure.Database;
using SupplyChain.Modules.Warehouses.Infrastructure.PublicApi;
using SupplyChain.Modules.Warehouses.Infrastructure.Warehouses;
using SupplyChain.Modules.Warehouses.Presentation.Orders;
using SupplyChain.Modules.Warehouses.PublicApi;

namespace SupplyChain.Modules.Warehouses.Infrastructure;
public static class WarehouseModule
{
    public static IServiceCollection AddWarehouseModule(this IServiceCollection services , IConfiguration config)
    {
        services.AddEndpoints(Presentation.AssemblyReference.Assembly);
        AddInfrastructure(services, config);
        return services;
    }
    public static void ConfigureConsumers(IRegistrationConfigurator registrationConfigurator)
    {
        registrationConfigurator.AddConsumer<NewCatalogOrderUpdateIntegrationEventConsumer>();
    }

    private static void AddInfrastructure(IServiceCollection services, IConfiguration config)
    {
        string databaseConnectionString = config.GetConnectionString("Database");

        services.AddDbContext<WarehouseDBContext>((sp,opt) =>
        {
            opt.UseNpgsql(databaseConnectionString, npgSqlOptions =>
            {


                npgSqlOptions.MigrationsHistoryTable(HistoryRepository.DefaultTableName, Schemas.Warehouse);


            }).UseUpperSnakeCaseNamingConvention()
            .AddInterceptors(sp.GetRequiredService<PublishDomainEventsInterceptor>());

        });


        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<WarehouseDBContext>());
        services.AddScoped<IWarehouseRepository, WarehouseRepository>();
        services.AddScoped<IBinLocationRepository, BinLocationRepository>();
        services.AddScoped<ICatalogRepository, CatalogRepository>();
        services.AddScoped<ICatalogQuantitiesRepository, CatalogQuantitiesRepository>();
        services.AddScoped<IWarehousePublicApi, WarehousePublicApi>();
    }
}
