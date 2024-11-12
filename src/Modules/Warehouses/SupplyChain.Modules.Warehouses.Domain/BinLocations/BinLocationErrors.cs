using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SupplyChain.Common.Domain;

namespace SupplyChain.Modules.Warehouses.Domain.BinLocations;
public  static class BinLocationErrors
{
    public static Error CodeExist(string Code) => 
        Error.Conflict("Binlocation.Exists", $"Binlocation with code {Code} already exist");
    public static Error NotFound() =>
     Error.NotFound("Binlocation.NotFound", $"Catalog not found found");
}
