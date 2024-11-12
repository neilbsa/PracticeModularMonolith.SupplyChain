using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SupplyChain.Common.Application.Messaging;
using SupplyChain.Common.Domain;
using SupplyChain.Modules.Warehouses.Domain.BinLocations;
using SupplyChain.Modules.Warehouses.Domain.BinLocations.Repository;
using SupplyChain.Modules.Warehouses.Domain.CatalogQuantities;
using SupplyChain.Modules.Warehouses.Domain.Warehouses;

namespace SupplyChain.Modules.Warehouses.Application.BinLocations.GetBinLocationById;
public record GetBinLocationByIdQuery(Guid id) : IQuery<BinlocationDto?>;


public class BinlocationDto
{
    public Guid Id { get;  set; }
    public string Code { get;  set; }
    public string Name { get;  set; }
}
