using Contracts.Common;

namespace Contracts.Dtos.System.V1;

public record OwnCompanyDto : CompanyBaseDto
{
    public Guid Id { get; init; }
}