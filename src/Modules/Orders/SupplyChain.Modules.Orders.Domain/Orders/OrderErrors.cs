using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SupplyChain.Common.Domain;

namespace SupplyChain.Modules.Orders.Domain.Orders;
public static class OrderErrors
{
    public static Error CatalogNotFound() => Error.NotFound("Catalog.NotFound", "The catalog item dont exist");

    public static Error CatalogNotWarehouseExist() => Error.NotFound("Catalog.NotFound", "The catalog dont exist do this warehouse");

    public static Error OrderNotFound() => Error.NotFound("Order.NotFound", "The order dont exist");

    public static Error UserNotFound() => Error.NotFound("Customer.NotFound", "The customer dont exist");
    public static Error WarehouseNotFound() => Error.NotFound("Warehouse.NotFound", "The warehouse dont exist");
}
