using System.Data.Common;
using Dapper;
using SupplyChain.Common.Application.Data;
using SupplyChain.Common.Application.Messaging;
using SupplyChain.Common.Domain;
using SupplyChain.Modules.Warehouses.Domain.BinLocations;

namespace SupplyChain.Modules.Warehouses.Application.BinLocations.GetBinLocations;
public sealed record GetBinLocationsQuery(int Limit, int OffSet) : IQuery<IReadOnlyList<BinLocationResponse>>;
