namespace Livetta.WebAPI.Services;

public static class WebSocketChannelBuilder
{
    public static string GetChatGroup(Guid chatId) => "chat-" + chatId;
}