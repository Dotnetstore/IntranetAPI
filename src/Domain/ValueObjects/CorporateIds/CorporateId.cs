using Domain.Models;
using Domain.Services;

namespace Domain.ValueObjects.CorporateIds;

public record struct CorporateId
{
    public string? Id { get; }

    private CorporateId(string? id)
    {
        Id = id;
    }

    public static Result<CorporateId?> Create(string? id)
    {
        CorporateId? corporateId = null;
        
        if (string.IsNullOrWhiteSpace(id))
            return corporateId;

        if (SwedishCorporateId.Valid(id))
        {
            var swedishCorporateId = new SwedishCorporateId(id);
            return new CorporateId(swedishCorporateId.VatNumber);
        }
        
        return Error.Validation("CorporateID", "Invalid corporate id");
    }
}