using Livetta.Domain.Contracts;

namespace Livetta.Infrastructure.Persistence.Entities;

public class ContactsEntity : Entity
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Phone { get; set; }

    public Guid ResidentId { get; set; }
    public ResidentEntity? Resident { get; set; }
}