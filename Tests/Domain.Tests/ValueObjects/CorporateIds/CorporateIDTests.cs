using Domain.Enums;
using Domain.ValueObjects.CorporateIds;
using FluentAssertions;
using FluentAssertions.Execution;

namespace Domain.Tests.ValueObjects.CorporateIds;

public class CorporateIDTests
{
    [Theory]
    [InlineData("556056-6258")]
    public void Should_return_correct_object(string id)
    {
        var corporateId = new CorporateId(id);

        using (new AssertionScope())
        {
            corporateId.Should().BeOfType<CorporateId>();
            corporateId.Id.Should().Be(id);
        }
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    public void Create_should_return_null(string value)
    {
        var result = CorporateId.Create(value);

        result.Should().BeNull();
    }

    [Theory]
    [InlineData("556056-6258")]
    public void Create_should_return_valid_object(string value)
    {
        var result = CorporateId.Create(value);

        using (new AssertionScope())
        {
            result!.Value.IsError.Should().BeFalse();
            result!.Value.Value.Should().NotBeNull();
        }
    }

    [Theory]
    [InlineData("556056-6259")]
    public void Create_should_return_validation_error(string value)
    {
        var result = CorporateId.Create(value);

        using (new AssertionScope())
        {
            result!.Value.IsError.Should().BeTrue();
            result!.Value.FirstError.Type.Should().Be(ErrorType.Validation);
        }
    }
}