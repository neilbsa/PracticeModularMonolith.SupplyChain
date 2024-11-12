
using Microsoft.EntityFrameworkCore;
using SupplyChain.Modules.Orders.Infrastructure.Database.Data;
using SupplyChain.Modules.Users.Infrastructure.Database;
using SupplyChain.Modules.Warehouses.Infrastructure.Database;

namespace SupplyChain.API.Extensions;

internal static class MigrationExtensions
{
    internal static void ApplyMigrations(this IApplicationBuilder app)
    {
        using IServiceScope scope = app.ApplicationServices.CreateScope();

        ApplyMigration<WarehouseDBContext>(scope);
        ApplyMigration<UserDBContext>(scope);
        ApplyMigration<OrdersDbContext>(scope);
    }

    private static void ApplyMigration<TDbContext>(IServiceScope scope)
        where TDbContext : DbContext
    {
        using TDbContext context = scope.ServiceProvider.GetRequiredService<TDbContext>();

        context.Database.Migrate();
    }
}
