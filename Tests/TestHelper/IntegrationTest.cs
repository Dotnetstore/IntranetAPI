using Application.Common.Interfaces;
using Domain.Entities.System;
using Infrastructure.Contexts;
using TestHelper.FakeData;

namespace TestHelper;

public abstract class IntegrationTest : IClassFixture<DotnetstoreApiFactory>, IAsyncLifetime
{
    protected readonly HttpClient Client;
    protected readonly CancellationToken CancellationToken;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ApplicationDataContext _applicationDataContext;

    protected IntegrationTest(DotnetstoreApiFactory fixture)
    {
        CancellationToken = new CancellationTokenSource().Token;
        Client = fixture.CreateClient();
        _unitOfWork = fixture.UnitOfWork;
        _applicationDataContext = fixture.ApplicationDataContext;
    }

    private async ValueTask LoadTestDatabaseAsync(CancellationToken cancellationToken)
    {
        _applicationDataContext.OwnCompanies.RemoveRange(_applicationDataContext.OwnCompanies);
        await _applicationDataContext.SaveChangesAsync(CancellationToken);
        
        var ownCompany = SystemFakeData.GenerateOwnCompanyFakeData(10);
    
        foreach (var company in ownCompany)
        {
            _unitOfWork.Repository<OwnCompany>().Create(company);
        }
    
        await _unitOfWork.SaveAsync(cancellationToken);
    }

    public async Task InitializeAsync()
    {
        await LoadTestDatabaseAsync(CancellationToken);
    }

    public async Task DisposeAsync()
    {
        Client.Dispose();
        await Task.CompletedTask;
    }
}