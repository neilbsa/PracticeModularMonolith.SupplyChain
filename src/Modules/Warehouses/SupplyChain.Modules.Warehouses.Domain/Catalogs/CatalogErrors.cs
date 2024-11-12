using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SupplyChain.Common.Domain;


namespace SupplyChain.Modules.Warehouses.Domain.Catalogs;
public static class CatalogErrors
{
    public static Error IdAlreadyExist(string catalogId) =>
        Error.Conflict("Catalog.Exist", $"Catalog Id {catalogId} already exists.");
    public static Error NotFound() =>
      Error.NotFound("Catalog.NotFound", $"Catalog not found");

}
