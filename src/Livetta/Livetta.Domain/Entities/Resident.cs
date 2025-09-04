using Livetta.Domain.Contracts;

namespace Livetta.Domain.Entities;

public class Resident : Entity, IResident
{
    readonly List<IApartment> _apartments;

    public Resident(IContacts contacts, IEnumerable<IApartment> apartments)
    {
        Contacts = contacts;
        _apartments = apartments.ToList();
    }

    public string FullName => Contacts.FirstName + " " + Contacts.LastName;
    public IContacts Contacts { get; }
    public IReadOnlyCollection<IApartment> Apartments => _apartments.AsReadOnly();
    
    public void AddApartment(IApartment apartment)
    {
        if (!_apartments.Contains(apartment))
            _apartments.Add(apartment);
        
        apartment.AddResident(this);
    }

    public bool RemoveApartment(IApartment apartment)
    {
        if (!_apartments.Remove(apartment)) return false;
        
        apartment.RemoveResidentAll(r => r == this);
        return true;
    }
}