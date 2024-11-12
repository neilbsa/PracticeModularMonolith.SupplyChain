using SupplyChain.Common.Application.Messaging;

namespace SupplyChain.Modules.Warehouses.Application.CatalogQuantities.AddCatalogToBinLocation;


public sealed record CatalogAddToBinLocationCommand(Guid CatalogId, Guid BinLocationId) : ICommand<Guid>;
