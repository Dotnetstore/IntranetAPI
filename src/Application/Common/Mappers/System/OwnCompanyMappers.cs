using Contracts.Dtos.System.V1;
using Domain.Entities.System;

namespace Application.Common.Mappers.System;

internal static class OwnCompanyMappers
{
    internal static OwnCompanyDto ToDto(this OwnCompany q)
    {
        return new OwnCompanyDto
        {
            ConcurrencyToken = q.ConcurrencyToken,
            CreatedDate = q.CreatedDate,
            CreatedBy = q.CreatedBy,
            DeletedDate = q.DeletedDate,
            DeletedBy = q.DeletedBy,
            IsDeleted = q.IsDeleted,
            IsGdpr = q.IsGdpr,
            IsSystem = q.IsSystem,
            LastUpdatedBy = q.LastUpdatedBy,
            LastUpdatedDate = q.LastUpdatedDate,
            CorporateId = q.CorporateId?.Id,
            Name = q.Name,
            Id = q.Id.Id
        };
    }
}