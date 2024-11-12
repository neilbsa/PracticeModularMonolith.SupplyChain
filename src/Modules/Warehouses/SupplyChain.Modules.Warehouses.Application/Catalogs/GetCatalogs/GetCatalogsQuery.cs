using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using SupplyChain.Common.Application.Data;
using SupplyChain.Common.Application.Messaging;
using SupplyChain.Common.Domain;

namespace SupplyChain.Modules.Warehouses.Application.Catalogs.GetCatalogs;
public sealed record GetCatalogsQuery(int Limit, int Offset) : IQuery<IReadOnlyList<CatalogResponse>>;
