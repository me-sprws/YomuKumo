using Livetta.Domain.Contracts;

namespace Livetta.Domain.Entities;

public class Resident : Entity, IResident
{
    public Resident(IContacts contacts) : base(Guid.NewGuid())
    {
        Contacts = contacts;
    }

    public IContacts Contacts { get; }
    public string FullName => Contacts.FirstName + " " + Contacts.LastName;
}