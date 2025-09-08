using System.Runtime.Serialization;

namespace Livetta.Core.Exceptions;

public class LivettaException : Exception
{
    public LivettaException()
    {
    }

    protected LivettaException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    public LivettaException(string? message) : base(message)
    {
    }

    public LivettaException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}