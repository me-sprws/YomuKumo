namespace Livetta.WebAPI.Services;

public static class SignalRGroupBuilder
{
    public static string GetChatGroup(Guid chatId)
    {
        return "chat-" + chatId;
    }
}