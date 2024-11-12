
using FluentValidation;
using SupplyChain.Common.Application.Messaging;
using SupplyChain.Common.Domain;
using SupplyChain.Modules.Warehouses.Domain.Catalogs;
using SupplyChain.Modules.Warehouses.Domain.Catalogs.Events;
using SupplyChain.Modules.Warehouses.Domain.Catalogs.Repository;

namespace SupplyChain.Modules.Warehouses.Application.Catalogs.CreateNewCatalog;


internal sealed class NewCatalogItemCreatedEventHandler : IDomainEventHandler<NewCatalogItemCreated>
{
    public Task Handle(NewCatalogItemCreated notification, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}

public sealed record CreateNewCatalogCommand(string CatalogId, string description, string category) : ICommand<Guid>;
