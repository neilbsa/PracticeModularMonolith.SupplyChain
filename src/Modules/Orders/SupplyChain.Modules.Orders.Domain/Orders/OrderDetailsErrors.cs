using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SupplyChain.Common.Domain;

namespace SupplyChain.Modules.Orders.Domain.Orders;
public static class OrderDetailsErrors
{
    public static Error InvalidQuantity() => Error.Conflict("Quantity.Invalid","The inputted quantity is invalid");
}
