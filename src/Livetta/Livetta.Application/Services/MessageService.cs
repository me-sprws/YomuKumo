using Livetta.Application.Contracts;
using Livetta.Application.DTO.Messages;
using Livetta.Application.Extensions.DTOs;
using Livetta.Application.Extensions.Repositories;
using Livetta.Domain.Entities.Messaging;
using Livetta.Domain.Repositories;

namespace Livetta.Application.Services;

public sealed class MessageService(
    IChatRepository chatRepository,
    IMessageRepository messageRepository,
    INotificationService notificationService
) : IMessageService
{
    public async Task<MessageReadDto[]> GetMessagesAsync(Guid chatId, int take, int offset)
    {
        var db = messageRepository.Get(new(OrderByCreation: true, ChatId: chatId, AsNoTracking: true));

        return (await messageRepository.ToListAsync(db.Skip(offset).Take(take))).Select(x => x.ToDto()).ToArray();
    }

    public async Task<MessageReadDto> CreateAsync(Guid chatId, Guid residentId, MessageCreateDto request)
    {
        var members = await chatRepository.FindChatMembersNoTrackingAsync(chatId);

        if (members is null)
            throw new InvalidOperationException("Chat has no members.");
        
        Message message = new()
        {
            ChatId = chatId,
            ResidentId = residentId,
            Text = request.Text
        };

        await messageRepository.AddAsync(message);
        await messageRepository.UnitOfWork.SaveChangesAsync();

        var messageDto = message.ToDto();
        
        await notificationService.OnChatMessageAsync(members.Select(m => m.ResidentId).ToArray(), messageDto);

        return messageDto;
    }
}