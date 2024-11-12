using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SupplyChain.Common.Domain;
using SupplyChain.Modules.Warehouses.Domain.CatalogQuantities;
using SupplyChain.Modules.Warehouses.Domain.Catalogs.Events;

namespace SupplyChain.Modules.Warehouses.Domain.Catalogs;
public sealed class Catalog : Entity
{
    private readonly List<CatalogQuantity> _quantities = [];
    private Catalog(Guid id, CatalogId catalogId, CatalogDescription description, CatalogCategory category, CatalogStatus status)
    {
        Id = id;
        CatalogId = catalogId;
        Description = description;
        Category = category;
        Status = status;
    }
    private Catalog()
    {
        
    }
    public Guid Id { get; set; }
    public CatalogId CatalogId { get; private set; }
    public CatalogDescription Description { get; private set; }
    public CatalogCategory Category { get; private set; }
    public CatalogStatus Status { get; private set; }   
    public IReadOnlyList<CatalogQuantity> Quantities  => _quantities.AsReadOnly();
    public static Catalog Create(string CatalogId, string description, string category)
    {
        var catalog = new Catalog(
                  Guid.NewGuid(),
                  new CatalogId(CatalogId),
                  new CatalogDescription(description),
                  new CatalogCategory(category),
                  CatalogStatus.Active
            );


        catalog.Raise(new NewCatalogItemCreated(catalog.Id));
        return catalog; 
    }
    public Result Active()
    {
        Status = CatalogStatus.Active;

        Raise(new NewCatalogReactivation(Id));
        return Result.Success();
    }
    public Result Deactivate()
    {
        Status = CatalogStatus.Inactive;

        Raise(new NewCatalogDeactivation(Id));
        return Result.Success();
    }
 
}
