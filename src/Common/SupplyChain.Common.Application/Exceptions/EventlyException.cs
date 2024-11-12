
using SupplyChain.Common.Domain;

namespace SupplyChain.Common.Application.Exceptions;

public sealed class WarehouseException : Exception
{
    public WarehouseException(string requestName, Error? error = default, Exception? innerException = default)
        : base("Application exception", innerException)
    {
        RequestName = requestName;
        Error = error;
    }

    public string RequestName { get; }

    public Error? Error { get; }
}
