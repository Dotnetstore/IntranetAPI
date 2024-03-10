using Domain.Models;
using Domain.Services;

namespace Domain.ValueObjects.CorporateIds;

public record struct CorporateID(string? Id)
{
    public string? Id { get; init; } = Id;

    public static Result<CorporateID>? Create(string? id)
    {
        if (string.IsNullOrWhiteSpace(id))
            return null;

        if (SwedishCorporateId.Valid(id))
        {
            var swedishCorporateId = new SwedishCorporateId(id);
            return new CorporateID(swedishCorporateId.VatNumber);
        }
        
        return Error.Validation("CorporateID", "Invalid corporate id");
    }
}