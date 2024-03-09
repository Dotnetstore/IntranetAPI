namespace Contracts.Common;

public record CompanyBaseDto : BaseAuditableEntityDto
{
    public required string Name { get; init; }

    public string? CorporateId { get; init; }
}