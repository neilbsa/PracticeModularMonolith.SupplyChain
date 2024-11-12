using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SupplyChain.Common.Application.Messaging;
using SupplyChain.Modules.Warehouses.Application.BinLocations.GetBinLocationById;
using SupplyChain.Modules.Warehouses.Domain.BinLocations;

namespace SupplyChain.Modules.Warehouses.Application.BinLocations.GetBinLocationByCode;
public sealed record GetBinLocationByCodeQuery(string code) : IQuery<BinlocationDto?>;
