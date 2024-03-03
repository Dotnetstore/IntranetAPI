namespace Domain.Common;

public abstract class CompanyBase : BaseAuditableEntity
{
    public required string Name { get; init; }

    public string? CorporateId { get; init; }
}