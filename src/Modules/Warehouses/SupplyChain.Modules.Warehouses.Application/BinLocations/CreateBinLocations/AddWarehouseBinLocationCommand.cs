using FluentValidation;
using SupplyChain.Common.Application.Messaging;
using SupplyChain.Common.Domain;
using SupplyChain.Modules.Warehouses.Domain.BinLocations;
using SupplyChain.Modules.Warehouses.Domain.BinLocations.Repository;
using SupplyChain.Modules.Warehouses.Domain.Warehouses;
using SupplyChain.Modules.Warehouses.Domain.Warehouses.Repository;

namespace SupplyChain.Modules.Warehouses.Application.BinLocations.CreateBinLocations;
public sealed record AddWarehouseBinLocationCommand(string BinlocationCode, string BinLocationName, Guid WarehouseId) : ICommand<Guid>;
