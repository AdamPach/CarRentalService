namespace CarRentalService.Domain.Entities.Persons;

public record Address(string Street, string City, string ZipCode)
{
    public static Address Empty => new Address(string.Empty, string.Empty, string.Empty);
}