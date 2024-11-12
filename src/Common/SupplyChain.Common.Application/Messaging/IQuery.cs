
using MediatR;
using SupplyChain.Common.Domain;

namespace SupplyChain.Common.Application.Messaging;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>;
