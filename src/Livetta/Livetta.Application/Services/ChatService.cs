using Livetta.Application.Contracts;
using Livetta.Application.DTO.Chats;
using Livetta.Application.Extensions.DTOs;
using Livetta.Domain.Entities.Messaging;
using Livetta.Domain.Repositories;

namespace Livetta.Application.Services;

public sealed class ChatService(IResidentRepository residentRepository, IChatRepository chatRepository) : IChatService
{
    public async Task<ChatReadDto> CreateAsync(Guid residentId, ChatCreateDto request)
    {
        var residentDb = residentRepository.Get(new(IncludeContacts: true));

        var resident = await residentRepository.FirstOrDefaultAsync(residentDb, residentId)
            ?? throw new InvalidOperationException("Resident not found.");
        
        Chat chat = new()
        {
            Residents =
            {
                new() { Resident = resident }
            }
        };
        
        await chatRepository.AddAsync(chat);
        await chatRepository.UnitOfWork.SaveChangesAsync();

        return chat.ToDto();
    }

    public async Task<ChatReadDto[]> GetAllAsync(Guid residentId)
    {
        var db = chatRepository.Get(new(ResidentId: residentId, IncludeResidents: true, IncludeLastMessage: true, AsNoTracking: true));
        return (await chatRepository.ToListAsync(db)).Select(x => x.ToDto()).ToArray();
    }
}