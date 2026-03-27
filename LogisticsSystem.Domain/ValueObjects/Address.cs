using LogisticsSystem.Domain.Common;

namespace LogisticsSystem.Domain.ValueObjects;

public class Address : ValueObject
{
    public string? Street { get; }
    public string? Ward { get; }
    public string? District { get; }
    public string? City { get; }
    public string? Country { get; }

    public Address(string? street, string? ward, string? district, string? city, string? country)
    {
        Street = street;
        Ward = ward;
        District = district;
        City = city;
        Country = country;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Street ?? string.Empty;
        yield return Ward ?? string.Empty;
        yield return District ?? string.Empty;
        yield return City ?? string.Empty;
        yield return Country ?? string.Empty;
    }
}