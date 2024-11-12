
using Evently.Common.Infrastructure.Interceptors;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SupplyChain.Common.Presentation.Endpoint;
using SupplyChain.Modules.Users.Application.Abstractions.Data;
using SupplyChain.Modules.Users.Domain.Users.Repository;
using SupplyChain.Modules.Users.Infrastructure.Database;
using SupplyChain.Modules.Users.Infrastructure.PublicApis;
using SupplyChain.Modules.Users.Infrastructure.Users;
using SupplyChain.Modules.Users.PublicApi;

namespace SupplyChain.Modules.Users.Infrastructure;
public static class UserModule
{
    public static IServiceCollection AddUserModule(this IServiceCollection services, IConfiguration config)
    {
        services.AddEndpoints(Presentation.AssemblyReference.Assembly);
        AddInfrastructure(services, config);
        return services;
    }

    private static void AddInfrastructure(IServiceCollection services, IConfiguration config)
    {
        string databaseConnectionString = config.GetConnectionString("Database");

        services.AddDbContext<UserDBContext>((sp, opt) =>
        {
            opt.UseNpgsql(databaseConnectionString, npgSqlOptions =>
            {
                npgSqlOptions.MigrationsHistoryTable(HistoryRepository.DefaultTableName, Schemas.Users);
            }).UseUpperSnakeCaseNamingConvention()
            .AddInterceptors(sp.GetRequiredService<PublishDomainEventsInterceptor>());
        });

        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<UserDBContext>());
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUserPublicApi, UserPublicApi>();
    }
}
