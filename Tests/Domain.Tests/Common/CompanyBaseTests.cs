using Domain.Common;
using FluentAssertions;
using FluentAssertions.Execution;

namespace Domain.Tests.Common;

public class CompanyBaseTests
{
    [Fact]
    public void Should_contain_correct_properties()
    {
        var item = new CompanyBaseClass
        {
            Name = "Name",
            CorporateId = "CorporateId"
        };

        using (new AssertionScope())
        {
            item.Name.Should().Be("Name");
            item.CorporateId.Should().Be("CorporateId");
        }
    }
}

public class CompanyBaseClass : CompanyBase{}