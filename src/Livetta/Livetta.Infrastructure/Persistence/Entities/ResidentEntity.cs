using Livetta.Domain.Contracts;

namespace Livetta.Infrastructure.Persistence.Entities;

public class ResidentEntity : Entity
{
    public ContactsEntity Contacts { get; set; }
}