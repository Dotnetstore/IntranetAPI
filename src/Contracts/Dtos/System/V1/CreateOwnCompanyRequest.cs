namespace Contracts.Dtos.System.V1;

public record struct CreateOwnCompanyRequest(
    string Name,
    string? CorporateId);