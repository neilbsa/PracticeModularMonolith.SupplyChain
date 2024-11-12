
using SupplyChain.Common.Application.Messaging;
using SupplyChain.Common.Domain;
using SupplyChain.Modules.Warehouses.Domain.Catalogs;
using SupplyChain.Modules.Warehouses.Domain.Catalogs.Repository;

namespace SupplyChain.Modules.Warehouses.Application.Catalogs.ActivateCatalog;
public sealed record ActivateCatalogCommand(Guid CatalogId) : ICommand;
