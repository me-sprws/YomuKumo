namespace Livetta.Domain.ValueObjects;

public readonly struct Phone
{
    readonly string _number;

    public Phone(string number)
    {
        // const string regex = @"^(?:\+7|8)\s?\(?\d{3}\)?[\s-]?\d{3}[\s-]?\d{2}[\s-]?\d{2}$";
        //
        // if (!Regex.IsMatch(number, regex))
        //     throw new InvalidOperationException("Invalid phone number.");

        _number = number;
    }
    
    // public static Phone CreateValid(string number)
    // {
    //     return new(number);
    // }

    public static Phone? CreateValidOrNull(string? number)
    {
        if (string.IsNullOrWhiteSpace(number))
            return null;

        return new(number);
    }

    public static Phone CreateValid(string? number)
    {
        return CreateValidOrNull(number) ?? throw new InvalidOperationException("Invalid input number.");
    }

    public override string ToString() => _number;
}