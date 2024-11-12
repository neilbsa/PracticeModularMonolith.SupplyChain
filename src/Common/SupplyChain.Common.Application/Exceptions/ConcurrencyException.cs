
using SupplyChain.Common.Domain;

namespace SupplyChain.Common.Application.Exceptions;
public sealed class ConcurrencyException : Exception
{
    public ConcurrencyException(string requestName, Error? error = default, Exception? innerException = default)
        : base("Concurrency exception", innerException)
    {
        RequestName = requestName;
        Error = error;
    }

    public string RequestName { get; }

    public Error? Error { get; }
}
