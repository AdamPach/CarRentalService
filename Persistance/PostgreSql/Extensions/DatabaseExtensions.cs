using System.Data.Common;
using CarRentalService.Domain.Persons.ValueObjects;

namespace CarRentalService.Persistence.PostgreSql.Extensions;

internal static class DatabaseExtensions
{
    internal static Address MapAddress(this DbDataReader reader)
    {
        return new Address(
            reader.GetString(reader.GetOrdinal("Street")),
            reader.GetString(reader.GetOrdinal("City")),
            reader.GetString(reader.GetOrdinal("ZipCode")));
    }
}