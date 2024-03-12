using Application.Commands.System.OwnCompanies.Create.V1;
using Application.Queries.System.OwnCompanies.GetAll.V1;
using Contracts.Dtos.System.V1;
using Domain.ValueObjects.CorporateIds;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Presentation.Swagger;

namespace Presentation.Controllers.System;

[ApiController]
public class OwnCompanyController(ISender sender) : ControllerBase
{
    [HttpGet(ApiEndPoints.System.V1.OwnCompany.GetAll)]
    [ProducesResponseType(typeof(IEnumerable<OwnCompanyDto>), StatusCodes.Status200OK)]
    public async ValueTask<IActionResult> GetAll([FromQuery] bool? isDeleted = null, CancellationToken cancellationToken = default)
    {
        var query = new GetAllOwnCompaniesQuery(isDeleted);

        var result = await sender.Send(query, cancellationToken);

        return Ok(result);
    }

    [HttpPost(ApiEndPoints.System.V1.OwnCompany.Create)]
    [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
    public async ValueTask<IActionResult> Create([FromBody] CreateOwnCompanyRequest request,
        CancellationToken cancellationToken = default)
    {
        var corporateIdResult = CorporateId.Create(request.CorporateId);

        if (corporateIdResult.IsError)
            return BadRequest(corporateIdResult.FirstError.Description);
        
        var command = new CreateOwnCompanyCommand(request.Name, corporateIdResult.Value);
        
        var result = await sender.Send(command, cancellationToken);
        
        return Ok(result.Value);
    }
}