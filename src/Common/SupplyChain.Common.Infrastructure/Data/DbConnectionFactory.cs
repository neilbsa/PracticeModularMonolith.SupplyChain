﻿using System.Data.Common;

using Npgsql;
using SupplyChain.Common.Application.Data;

namespace SupplyChain.Common.Infrastructure.Data;

internal sealed class DbConnectionFactory(NpgsqlDataSource dataSource) : IDbConnectionFactory
{
    public async ValueTask<DbConnection> OpenConnectionAsync()
    {
        return await dataSource.OpenConnectionAsync();
    }
}
