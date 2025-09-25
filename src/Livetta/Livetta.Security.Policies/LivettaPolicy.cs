namespace Livetta.Security.Policies;

public static class LivettaPolicy
{
    public static class Messaging
    {
        const string Namespace = nameof(Messaging);
        
        public const string CanCreateChats = $"{Namespace}.{nameof(CanCreateChats)}";
        public const string ChatMember = $"{Namespace}.{nameof(ChatMember)}";
    }
}