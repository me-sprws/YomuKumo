namespace Livetta.Core.Extensions;

public static class GuidExtensions
{
    public static string Shortify(this Guid guid)
    {
        return guid.ToString().Split('-')[0];
    }
    
    public static string? Shortify(this Guid? guid)
    {
        return guid.ToString()?.Split('-')[0];
    }
}