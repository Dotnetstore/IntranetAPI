using Domain.Enums;
using Domain.ValueObjects.CorporateIds;
using FluentAssertions;
using FluentAssertions.Execution;

namespace Domain.Tests.ValueObjects.CorporateIds;

public class CorporateIDTests
{
    [Theory]
    [InlineData("556056-6258", "SE556056625801")]
    public void Should_return_correct_object(string id, string expected)
    {
        var corporateId = CorporateId.Create(id);

        using (new AssertionScope())
        {
            corporateId!.Value.Value.Should().BeOfType<CorporateId>();
            corporateId!.Value.Value.Id.Should().Be(expected);
        }
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    public void Create_should_return_null(string value)
    {
        var result = CorporateId.Create(value);

        result.Value.Should().BeNull();
    }

    [Theory]
    [InlineData("556056-6258")]
    public void Create_should_return_valid_object(string value)
    {
        var result = CorporateId.Create(value);

        using (new AssertionScope())
        {
            result.IsError.Should().BeFalse();
            result!.Value!.Value.Should().NotBeNull();
        }
    }

    [Theory]
    [InlineData("556056-6259")]
    public void Create_should_return_validation_error(string value)
    {
        var result = CorporateId.Create(value);

        using (new AssertionScope())
        {
            result!.IsError.Should().BeTrue();
            result!.FirstError.Type.Should().Be(ErrorType.Validation);
        }
    }
}