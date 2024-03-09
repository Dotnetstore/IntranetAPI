using System.Net;
using System.Net.Http.Json;
using Contracts.Dtos.System.V1;
using FluentAssertions;
using Newtonsoft.Json;
using Presentation.Swagger;
using TestHelper;

namespace Integration.Tests.System;

public class OwnCompanyControllerTests(DotnetstoreApiFactory factory) : IntegrationTest(factory)
{
    [Fact]
    public async Task GetOwnCompany_should_return_HttpStatusCode_OK()
    {
        var response = await Client.GetAsync(ApiEndPoints.System.V1.OwnCompany.GetAll);
        
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task GetAll_should_return_10_objects()
    {
        var response = await Client.GetAsync(ApiEndPoints.System.V1.OwnCompany.GetAll);
        var result = await response.Content.ReadAsStringAsync(CancellationToken);
        var list = JsonConvert.DeserializeObject<IEnumerable<OwnCompanyDto>>(result);

        list.Should().HaveCount(10);
    }

    [Fact]
    public async Task GetAll_should_return_not_deleted()
    {
        var response = await Client.GetAsync(ApiEndPoints.System.V1.OwnCompany.GetAll + "?isDeleted=false");
        var result = await response.Content.ReadAsStringAsync(CancellationToken);
        var list = JsonConvert.DeserializeObject<IEnumerable<OwnCompanyDto>>(result);

        var deleted = list.Where(q => q.IsDeleted);
        deleted.Should().HaveCount(0);
    }

    [Fact]
    public async Task GetAll_should_return_deleted()
    {
        var response = await Client.GetAsync(ApiEndPoints.System.V1.OwnCompany.GetAll + "?isDeleted=true");
        var result = await response.Content.ReadAsStringAsync(CancellationToken);
        var list = JsonConvert.DeserializeObject<IEnumerable<OwnCompanyDto>>(result);

        var notDeleted = list.Where(q => !q.IsDeleted);
        notDeleted.Should().HaveCount(0);
    }
    
    [Fact]
    public async Task Create_should_return_HttpStatusCode_OK()
    {
        var request = new CreateOwnCompanyRequest("Test", null);
        
        var response = await Client.PostAsJsonAsync(ApiEndPoints.System.V1.OwnCompany.Create, request);
        
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}