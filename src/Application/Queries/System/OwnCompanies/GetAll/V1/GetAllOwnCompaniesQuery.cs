using Contracts.Dtos.System.V1;
using MediatR;

namespace Application.Queries.System.OwnCompanies.GetAll.V1;

public record struct GetAllOwnCompaniesQuery(bool? IsDeleted) : IRequest<IEnumerable<OwnCompanyDto>>;