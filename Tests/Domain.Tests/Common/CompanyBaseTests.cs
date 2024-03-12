using Domain.Common;
using Domain.ValueObjects.CorporateIds;
using FluentAssertions;
using FluentAssertions.Execution;

namespace Domain.Tests.Common;

public class CompanyBaseTests
{
    [Fact]
    public void Should_contain_correct_properties()
    {
        var corporateId = CorporateId.Create("7101263924").Value;
        
        var item = new CompanyBaseClass
        {
            Name = "Name",
            CorporateId = corporateId
        };

        using (new AssertionScope())
        {
            item.Name.Should().Be("Name");
            item.CorporateId!.Value.Id.Should().Be("SE710126392401");
        }
    }
}

public class CompanyBaseClass : CompanyBase{}