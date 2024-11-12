
using System.Diagnostics;
using Evently.Common.Infrastructure.Interceptors;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Npgsql;
using StackExchange.Redis;
using SupplyChain.Common.Application.Caching;
using SupplyChain.Common.Application.Clock;
using SupplyChain.Common.Application.Data;
using SupplyChain.Common.Application.EventBux;
using SupplyChain.Common.Infrastructure.Caching;
using SupplyChain.Common.Infrastructure.Clock;
using SupplyChain.Common.Infrastructure.Data;
using SupplyChain.Common.Infrastructure.EventBux;

namespace SupplyChain.Common.Infrastructure;

public static class InfrastructureConfiguration
{
  
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
             Action<IRegistrationConfigurator>[] moduleConfigureConsumers,
        string databaseConnectionString,
        string redisConnectionString)
    {
        NpgsqlDataSource npgsqlDataSource = new NpgsqlDataSourceBuilder(databaseConnectionString).Build();
        services.TryAddSingleton(npgsqlDataSource);
        services.AddScoped<IDbConnectionFactory, DbConnectionFactory>();

        services.TryAddSingleton<PublishDomainEventsInterceptor>();


        services.TryAddSingleton<IDateTimeProvider, DateTimeProvider>();
        IConnectionMultiplexer connectionMultiplexer = ConnectionMultiplexer.Connect(redisConnectionString);
        services.TryAddSingleton(connectionMultiplexer);

        services.AddStackExchangeRedisCache(options =>
            options.ConnectionMultiplexerFactory = () => Task.FromResult(connectionMultiplexer));


        services.TryAddSingleton<ICacheService, CacheService>();
        services.TryAddScoped<IEventBus, EventBus>();


        services.AddMassTransit(config =>
        {
            foreach (Action<IRegistrationConfigurator> configureConsumer in moduleConfigureConsumers)
            {
                configureConsumer(config);
            }
            config.SetKebabCaseEndpointNameFormatter();
            config.UsingInMemory((ctx, cfg) =>
            {


                cfg.ConfigureEndpoints(ctx);

            });



        });


        return services;
    }
}
