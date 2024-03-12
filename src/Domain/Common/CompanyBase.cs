using Domain.ValueObjects.CorporateIds;

namespace Domain.Common;

public abstract class CompanyBase : BaseAuditableEntity
{
    public required string Name { get; init; }

    public CorporateId? CorporateId { get; init; }
}