using Application.Common.Interfaces;
using Application.Common.Services;
using Domain.Entities.System;
using MediatR;

namespace Application.Commands.System.OwnCompanies.Create.V1;

internal sealed class CreateOwnCompanyCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<CreateOwnCompanyCommand, Result<bool>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

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
        
        _unitOfWork.Repository<OwnCompany>().Create(company);
        await _unitOfWork.SaveAsync(cancellationToken);

        return true;
    }
}