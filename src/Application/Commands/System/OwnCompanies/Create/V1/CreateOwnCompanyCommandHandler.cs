﻿using Application.Common.Interfaces;
using Domain.Entities.System;
using Domain.Services;
using MediatR;

namespace Application.Commands.System.OwnCompanies.Create.V1;

internal sealed class CreateOwnCompanyCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<CreateOwnCompanyCommand, Result<bool>>
{
    async Task<Result<bool>> IRequestHandler<CreateOwnCompanyCommand, Result<bool>>
        .Handle(CreateOwnCompanyCommand request, CancellationToken cancellationToken)
    {
        var company = new OwnCompany
        {
            CorporateId = request.CorporateId,
            CreatedBy = null,
            CreatedDate = DateTimeOffset.Now,
            DeletedBy = null,
            DeletedDate = null,
            Id = new OwnCompanyId(Guid.NewGuid()),
            IsDeleted = false,
            IsGdpr = false,
            IsSystem = false,
            LastUpdatedBy = null,
            LastUpdatedDate = null,
            Name = request.Name
        };
        
        unitOfWork.Repository<OwnCompany>().Create(company);
        await unitOfWork.SaveAsync(cancellationToken);

        return true;
    }
}