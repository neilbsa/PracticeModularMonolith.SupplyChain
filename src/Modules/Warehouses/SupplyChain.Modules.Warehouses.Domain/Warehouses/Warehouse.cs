using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SupplyChain.Common.Domain;

using SupplyChain.Modules.Warehouses.Domain.BinLocations;
using SupplyChain.Modules.Warehouses.Domain.Warehouses.Events;

namespace SupplyChain.Modules.Warehouses.Domain.Warehouses;
public sealed class Warehouse :Entity
{
    private readonly List<BinLocation> _binlocations = new List<BinLocation>();

    private Warehouse()
    {
        
    }
    private Warehouse(Guid id, WarehouseCode code, WarehouseDescription description, WarehouseAddress address)
    {
        Id = id;
        Code = code;
        Description = description;
        Address = address;
    }
    public ICollection<BinLocation> BinLocations => _binlocations;
    public Guid Id { get; private set; }
    public WarehouseCode Code { get; private set; }
    public WarehouseDescription Description { get; private set; }
    public WarehouseAddress Address { get; private set; }
    
    public static Warehouse Create(string Code, string Description, string Street, string City, string ZipCode, string Country)
    {
        var warehouse = new Warehouse(
                Guid.NewGuid(),
                new WarehouseCode(Code),
                new WarehouseDescription(Description),
                new WarehouseAddress(Street, City, ZipCode, Country)
            );
        warehouse.Raise(new NewWarehouseCreated(warehouse.Id));
        return warehouse;
    }
}

