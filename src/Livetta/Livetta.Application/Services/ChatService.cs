using Livetta.Application.Contracts;
using Livetta.Application.DTO.Chats;
using Livetta.Application.Extensions.DTOs;
using Livetta.Domain.Entities.Messaging;
using Livetta.Domain.Repositories;

namespace Livetta.Application.Services;

public sealed class ChatService(
    INotificationService notificationService,
    IResidentRepository residentRepository, 
    IChatRepository chatRepository
) : IChatService
{
    public async Task CreateChatMemberAsync(Guid chatId, ChatMemberCreateDto request)
    {
        var resident = await residentRepository.FirstOrDefaultAsync(residentRepository.QueryableSet, request.ResidentId)
            ?? throw new InvalidOperationException("Resident not found.");
        
        var chat = await chatRepository.FirstOrDefaultAsync(chatRepository.Get(new(IncludeChatMembers: true)), chatId)
            ?? throw new InvalidOperationException("Chat not found.");;
        
        chat.Residents.Add(new()
        {
            Resident = resident,
            MemberKind = request.MemberKind
        });

        await chatRepository.UnitOfWork.SaveChangesAsync();
        
        await notificationService.OnChatCreationAsync([request.ResidentId], chat.ToDto());
    }

    public async Task DeleteChatMemberAsync(Guid chatId, ChatMemberDeleteDto request)
    {
        var chat = await chatRepository.FirstOrDefaultAsync(chatRepository.Get(new(IncludeChatMembers: true)), chatId)
            ?? throw new InvalidOperationException("Chat not found.");

        var member = chat.Residents.FirstOrDefault(r => r.ResidentId == request.ResidentId);
        
        if (member is null)
            throw new InvalidOperationException("Chat member not found.");

        chat.Residents.Remove(member);
        await chatRepository.UnitOfWork.SaveChangesAsync();
        
        await notificationService.OnChatDeletionAsync([request.ResidentId], chatId);
    }

    public async Task<ChatReadDto> CreateAsync(Guid residentId, ChatCreateDto request)
    {
        var residentDb = residentRepository.Get(new(IncludeContacts: true));

        var resident = await residentRepository.FirstOrDefaultAsync(residentDb, residentId)
            ?? throw new InvalidOperationException("Resident not found.");
        
        Chat chat = new()
        {
            Residents =
            {
                new()
                {
                    MemberKind = ChatMemberKind.Owner,
                    Resident = resident
                }
            }
        };
        
        await chatRepository.AddAsync(chat);
        await chatRepository.UnitOfWork.SaveChangesAsync();

        var readDto = chat.ToDto();
        
        await notificationService.OnChatCreationAsync([residentId], readDto);

        return readDto;
    }

    public async Task DeleteAsync(Guid chatId)
    {
        var chat = await chatRepository.FirstOrDefaultAsync(chatRepository.Get(new(IncludeChatMembers: true)), chatId)
            ?? throw new InvalidOperationException("Chat not found.");

        var membersId = chat.Residents.Select(r => r.ResidentId);
        
        await chatRepository.DeleteAsync(chat);
        await chatRepository.UnitOfWork.SaveChangesAsync();
        
        await notificationService.OnChatDeletionAsync(membersId.ToArray(), chat.Id);
    }

    public async Task<ChatReadDto[]> GetAllAsync(Guid residentId)
    {
        var db = chatRepository.Get(new(ResidentId: residentId, IncludeResidents: true, IncludeLastMessage: true, AsNoTracking: true));
        return (await chatRepository.ToListAsync(db)).Select(x => x.ToDto()).ToArray();
    }
}