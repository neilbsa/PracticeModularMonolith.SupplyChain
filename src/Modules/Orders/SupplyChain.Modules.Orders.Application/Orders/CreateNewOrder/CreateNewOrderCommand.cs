using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SupplyChain.Common.Application.Messaging;
using SupplyChain.Common.Domain;
using SupplyChain.Modules.Orders.Application.Abstractions.Data;
using SupplyChain.Modules.Orders.Domain.Orders;
using SupplyChain.Modules.Orders.Domain.Orders.Repository;

using SupplyChain.Modules.Users.PublicApi;

using SupplyChain.Modules.Warehouses.PublicApi;


namespace SupplyChain.Modules.Orders.Application.Orders.CreateNewOrder;
public sealed record CreateNewOrderCommand(Guid warehouseId, Guid CustomerId) : ICommand<Guid>;
