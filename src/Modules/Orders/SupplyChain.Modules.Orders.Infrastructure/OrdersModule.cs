using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Evently.Common.Infrastructure.Interceptors;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using SupplyChain.Common.Presentation.Endpoint;
using SupplyChain.Modules.Orders.Application.Abstractions.Data;
using SupplyChain.Modules.Orders.Domain.Orders.Repository;

using SupplyChain.Modules.Orders.Infrastructure.Database.Data;
using SupplyChain.Modules.Orders.Infrastructure.Orders;
using SupplyChain.Modules.Users.PublicApi;

namespace SupplyChain.Modules.Orders.Infrastructure;
public static class OrdersModule
{
    public static IServiceCollection AddOrdersModule(this IServiceCollection services, IConfiguration config)
    {
        services.AddEndpoints(Presentation.AssemblyReference.Assembly);
        AddInfrastructure(services, config);
        return services;
    }

    private static void AddInfrastructure(IServiceCollection services, IConfiguration config)
    {
        string databaseConnectionString = config.GetConnectionString("Database");

        services.AddDbContext<OrdersDbContext>((sp, opt) =>
        {
            opt.UseNpgsql(databaseConnectionString, npgSqlOptions =>
            {
                npgSqlOptions.MigrationsHistoryTable(HistoryRepository.DefaultTableName, Schemas.Orders);
            }).UseUpperSnakeCaseNamingConvention()
            .AddInterceptors(sp.GetRequiredService<PublishDomainEventsInterceptor>());
        });
    

        services.AddScoped<IOrderRepository, OrderRepository>();

        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<OrdersDbContext>());

    }
}
