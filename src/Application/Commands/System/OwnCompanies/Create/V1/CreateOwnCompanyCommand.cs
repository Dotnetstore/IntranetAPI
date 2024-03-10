using Domain.Services;
using MediatR;

namespace Application.Commands.System.OwnCompanies.Create.V1;

public record struct CreateOwnCompanyCommand(string Name, string? CorporateId) : IRequest<Result<bool>>;