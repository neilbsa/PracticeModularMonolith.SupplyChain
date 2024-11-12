using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SupplyChain.Common.Application.Messaging;
using SupplyChain.Common.Domain;
using SupplyChain.Modules.Warehouses.Application.Warehouses.GetAllWarehouses;
using SupplyChain.Modules.Warehouses.Domain.Warehouses;
using SupplyChain.Modules.Warehouses.Domain.Warehouses.Repository;

namespace SupplyChain.Modules.Warehouses.Application.Warehouses.GetWarehouseByCode;
public sealed record GetWarehouseByCodeQuery(string Code) : IQuery<WarehouseResponse>;

