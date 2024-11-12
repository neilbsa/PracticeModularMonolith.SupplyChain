using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using SupplyChain.Common.Application.Messaging;

namespace SupplyChain.Modules.Warehouses.Application.CatalogQuantities.AddOnhandQuantity;
public sealed record AddOnHandCommand(Guid CatalogId,Guid BinLocationId, decimal quantity) : ICommand;
