using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using SupplyChain.Common.Application.Messaging;
using SupplyChain.Common.Domain;
using SupplyChain.Modules.Warehouses.Domain.BinLocations.Repository;
using SupplyChain.Modules.Warehouses.Domain.BinLocations;
using SupplyChain.Modules.Warehouses.Domain.CatalogQuantities.Repository;
using SupplyChain.Modules.Warehouses.Domain.CatalogQuantities;
using SupplyChain.Modules.Warehouses.Domain.Catalogs.Repository;
using SupplyChain.Modules.Warehouses.Domain.Catalogs;
using FluentValidation;

namespace SupplyChain.Modules.Warehouses.Application.CatalogQuantities.DeductOnHandQuantity;
public sealed record DeductOnHandCommand(Guid CatalogId, Guid BinLocationId, decimal Quantity) : ICommand;
