using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SupplyChain.Common.Application.Messaging;
using SupplyChain.Common.Domain;
using SupplyChain.Modules.Warehouses.Application.Catalogs.GetCatalogs;
using SupplyChain.Modules.Warehouses.Domain.Catalogs;
using SupplyChain.Modules.Warehouses.Domain.Catalogs.Repository;

namespace SupplyChain.Modules.Warehouses.Application.Catalogs.GetCatalogById;
public sealed record GetCatalogByIdQuery(Guid id) : IQuery<CatalogResponse>;
