
using MediatR;
using SupplyChain.Common.Domain;

namespace SupplyChain.Common.Application.Messaging;


public interface IDomainEventHandler<in TDomainEvent> : INotificationHandler<TDomainEvent>
    where TDomainEvent : IDomainEvent;
