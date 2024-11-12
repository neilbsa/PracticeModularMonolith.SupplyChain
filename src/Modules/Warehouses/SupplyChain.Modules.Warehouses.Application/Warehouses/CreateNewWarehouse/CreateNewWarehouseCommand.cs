using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using SupplyChain.Common.Application.Messaging;
using SupplyChain.Common.Domain;
using SupplyChain.Modules.Warehouses.Domain.Warehouses;
using SupplyChain.Modules.Warehouses.Domain.Warehouses.Repository;

namespace SupplyChain.Modules.Warehouses.Application.Warehouses.CreateNewWarehouse;
public sealed record CreateNewWarehouseCommand(
    string Code, 
    string Description, 
    string Street, 
    string City, 
    string ZipCode, 
    string Country
) : ICommand<Guid>;
