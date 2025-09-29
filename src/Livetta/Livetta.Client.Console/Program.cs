using Livetta.Application.DTO.Chats;
using Livetta.Application.DTO.Messages;
using Livetta.Client.Console;
using Livetta.Core.Extensions;
using Microsoft.AspNetCore.SignalR.Client;

Console.WriteLine("Starting.");

var client = new LivettaClient();

Console.WriteLine("Login.");

await client.LoginAsync(Guid.Parse("01992b6c-fb6f-7ca6-a0fb-9a2eeff0bdeb"));

Console.WriteLine("Logged in.");

var connection = new HubConnectionBuilder()
    .WithUrl(client.ChatHubUri, op =>
    {
        op.AccessTokenProvider = () => Task.FromResult(client.JwtToken);
    })
    .WithAutomaticReconnect()
    .Build();

connection.On<MessageReadDto>("OnChatMessage", PrintMessage);

connection.On<Guid>("OnChatDeletion", chatId =>
{
    PrintChatDeletion(chatId);
    
    // Отписываемся от уведомлений чата:
    connection.SendAsync("DisconnectFromChatGroup", chatId);
});

connection.On<ChatReadDto>("OnChatCreation", readDto =>
{
    PrintChatCreation(readDto);

    // Подписываемся на уведомления чата:
    connection.SendAsync("ConnectToChatGroup", readDto.Id);
});

await connection.StartAsync();

Console.WriteLine("Started SignalR.");

var messages = await client.GetMessagesAsync(Guid.Parse("019944d7-6656-77cd-ad7f-24bbdc2a5e78"), 15, 0);

foreach (var m in messages.OrderBy(m => m.CreatedAt))
    PrintMessage(m);

Console.WriteLine();

do
{
    var message = Console.ReadLine();

    if (!string.IsNullOrWhiteSpace(message))
        await client.SendMessageAsync(Guid.Parse("019944d7-6656-77cd-ad7f-24bbdc2a5e78"), message);
    
    Console.WriteLine();

} while (true);

static void PrintMessage(MessageReadDto readDto)
{
    Console.WriteLine($"[c:{readDto.ChatId.Shortify()}] [r:{readDto.ResidentId.Shortify()}] {readDto.Text}");
}

static void PrintChatCreation(ChatReadDto readDto)
{
    Console.WriteLine($"ПОЛЬЗОВАТЕЛЬ (ВЫ) ДОБАВЛЕН В ЧАТ {readDto.Id.Shortify()}!");
}

static void PrintChatDeletion(Guid chatId)
{
    Console.WriteLine($"ПОЛЬЗОВАТЕЛЬ (ВЫ) БЫЛ ИЗГНАН ИЗ ЧАТА {chatId.Shortify()}!");
}