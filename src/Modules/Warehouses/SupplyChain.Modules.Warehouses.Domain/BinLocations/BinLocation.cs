using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SupplyChain.Common.Domain;
using SupplyChain.Modules.Warehouses.Domain.BinLocations.Events;
using SupplyChain.Modules.Warehouses.Domain.CatalogQuantities;
using SupplyChain.Modules.Warehouses.Domain.Warehouses;
namespace SupplyChain.Modules.Warehouses.Domain.BinLocations;
public sealed class BinLocation : Entity
{

    private readonly List<CatalogQuantity> _quantities = [];
    private BinLocation(Guid id,BinLocationCode code, BinLocationName name,Guid _warehouseId)
    {
        Code = code;
        Name = name;
        Id = id;
        WarehouseId = _warehouseId;
    }

    private BinLocation()
    {
        
    }
    public Guid Id { get; private set; }
    public Guid WarehouseId { get; private set; }
    public Warehouse Warehouse { get; private set; }
    public IReadOnlyList<CatalogQuantity> Quantities => _quantities.AsReadOnly();
    public BinLocationCode Code { get; private set; }
    public BinLocationName Name{ get; private set; }
    
    public static BinLocation Create(string Code, string Name,Guid WarehouseId)
    {
        var location = new BinLocation(Guid.NewGuid(),new BinLocationCode(Code), new BinLocationName(Name), WarehouseId);
        location.Raise(new NewBinLocationCreated(location.Id));
        return location;
    }
}
