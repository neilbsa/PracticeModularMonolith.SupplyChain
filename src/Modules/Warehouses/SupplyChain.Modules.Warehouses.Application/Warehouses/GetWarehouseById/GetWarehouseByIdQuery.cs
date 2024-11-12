using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using SupplyChain.Common.Application.Messaging;
using SupplyChain.Common.Domain;
using SupplyChain.Modules.Warehouses.Domain.Warehouses;
using SupplyChain.Modules.Warehouses.Domain.Warehouses.Repository;

namespace SupplyChain.Modules.Warehouses.Application.Warehouses.GetWarehouseById;
public sealed record GetWarehouseByIdQuery(Guid Id) : IQuery<WarehouseByIdResponse?>;
