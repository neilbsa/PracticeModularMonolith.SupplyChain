
using SupplyChain.Common.Domain;

namespace SupplyChain.Modules.Warehouses.Domain.CatalogQuantities;
public static class CatalogQuantityErrors
{
    public static Error AlreadyTagged() =>
           Error.Problem("Catalog.Tagged", "Catalog already exists to this Warehouse\binlocation");

    public static Error NotFound() => 
        Error.NotFound("Quantity.NotFound ", "Catalog not yet exists to this binlocation");
    public static Error DeductInvalid() =>
    Error.NotFound("Quantity.DeductInvalid ", "Negative deduction difference is not valid");

}
