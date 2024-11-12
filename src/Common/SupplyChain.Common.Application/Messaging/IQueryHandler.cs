
using MediatR;
using SupplyChain.Common.Domain;

namespace SupplyChain.Common.Application.Messaging;

public interface IQueryHandler<in TQuery, TResponse> : IRequestHandler<TQuery, Result<TResponse>>
    where TQuery : IQuery<TResponse>;
