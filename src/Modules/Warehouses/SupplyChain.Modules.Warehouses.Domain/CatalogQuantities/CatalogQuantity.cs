using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SupplyChain.Common.Domain;
using SupplyChain.Modules.Warehouses.Domain.BinLocations;
using SupplyChain.Modules.Warehouses.Domain.CatalogQuantities.Events;
using SupplyChain.Modules.Warehouses.Domain.Catalogs;

namespace SupplyChain.Modules.Warehouses.Domain.CatalogQuantities;
public sealed class CatalogQuantity :Entity
{
    private CatalogQuantity()
    {
        
    }
    private CatalogQuantity(Guid id, Guid binLocationId, Guid catalogId, Quantity onHand)
    {
        Id = id;
        BinLocationId = binLocationId;
        CatalogId = catalogId;
        OnHand = onHand;
        Reserved = new Quantity(0);
    }

    public Guid Id { get;private set; }
    public Guid BinLocationId { get; private set; }
    public BinLocation Location { get; set; }
    public Guid CatalogId { get; private set; }
    public Catalog Catalog { get; set; }
    public Quantity OnHand { get; private set; } = new Quantity(0);
    public Quantity Reserved { get; private set; } = new Quantity(0); 

    public static CatalogQuantity Create(Guid BinLocationId,Guid CatalogId,decimal initialValue)
    {
        var catalogQuantity = new
                    CatalogQuantity(
                    Guid.NewGuid(),
                    BinLocationId,
                    CatalogId,
                    new Quantity(initialValue)
              );

    
        catalogQuantity.Raise(new NewCatalogQuantityCreated(catalogQuantity.Id));
        return catalogQuantity;
    }
    public Result AddOnHand(Quantity onhandQuantity)
    {
        OnHand += onhandQuantity;

        Raise(new OnHandQuantityUpdated(Id));

        return Result.Success();
    }
    public Result DeductOnHand(Quantity onhandQuantity)
    {
        try
        {
            OnHand -= onhandQuantity;
            Raise(new OnHandQuantityUpdated(Id));
        } 
        catch
        {
            return Result.Failure(CatalogQuantityErrors.DeductInvalid());
        }
        return Result.Success();
    }
 
}
