using Domain.Services;
using Domain.ValueObjects.CorporateIds;
using MediatR;

namespace Application.Commands.System.OwnCompanies.Create.V1;

public record struct CreateOwnCompanyCommand(string Name, CorporateId? CorporateId) : IRequest<Result<bool>>;