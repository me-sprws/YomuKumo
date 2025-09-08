namespace Livetta.Core.Exceptions;

public class NotFoundException : LivettaException
{
    public NotFoundException(Exception? inner) : base("Entity not found.", inner)
    {
        
    }

    public NotFoundException(string message) : base(message)
    {
        
    }
    
    public NotFoundException(string? message, string? entity, object? findKey, Exception? inner = null) : base(message, inner)
    {
    }
}