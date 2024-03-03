using Domain.Common;

namespace Domain.Entities.System;

public sealed class OwnCompany : CompanyBase
{
    public OwnCompanyId Id { get; set; }
}

public record struct OwnCompanyId(Guid Id);