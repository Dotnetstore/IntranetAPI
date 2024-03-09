using Application.Common.Interfaces;
using Application.Common.Mappers.System;
using Application.Extensions;
using Contracts.Dtos.System.V1;
using Domain.Entities.System;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Queries.System.OwnCompanies.GetAll.V1;

internal sealed class GetAllOwnCompaniesQueryHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<GetAllOwnCompaniesQuery, IEnumerable<OwnCompanyDto>>
{
    async Task<IEnumerable<OwnCompanyDto>> IRequestHandler<GetAllOwnCompaniesQuery, IEnumerable<OwnCompanyDto>>
        .Handle(GetAllOwnCompaniesQuery request, CancellationToken cancellationToken)
    {
        return await unitOfWork
            .Repository<OwnCompany>()
            .Entities
            .AsNoTracking()
            .WhereNullable(request.IsDeleted, q => q.IsDeleted == request.IsDeleted)
            .OrderBy(q => q.Name)
            .Select(q => q.ToDto())
            .ToListAsync(cancellationToken);
    }
}