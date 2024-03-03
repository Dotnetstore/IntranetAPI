using Domain.Entities.System;
using FluentAssertions;

namespace Domain.Tests.Entities.System;

public class OwnCompanyTests
{
    [Fact]
    public void Should_contain_correct_properties()
    {
        var item = new OwnCompany
        {
            Id = new OwnCompanyId(),
            Name = "OwnCompany"
        };

        item.Id.Should().NotBeNull();
    }

    [Fact]
    public void OwnCompanyId_should_return_correct_data()
    {
        var item = Guid.NewGuid();
        var id = new OwnCompanyId(item);

        id.Id.Should().Be(item);
    }
}