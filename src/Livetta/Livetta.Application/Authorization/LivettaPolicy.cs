namespace Livetta.Application.Authorization;

public static class LivettaPolicy
{
    public static class Messaging
    {
        const string Namespace = nameof(Messaging);
        
        public const string ChatMember = $"{Namespace}.{nameof(ChatMember)}";
    }
}