namespace CarRentalService.Persistence.PostgreSql.Models;

internal record SqlQueryParams(string Query, IDictionary<string, string> Parameters);