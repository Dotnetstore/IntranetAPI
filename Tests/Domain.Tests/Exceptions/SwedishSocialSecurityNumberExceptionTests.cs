using Domain.Exceptions;
using FluentAssertions;

namespace Domain.Tests.Exceptions;

public class SwedishSocialSecurityNumberExceptionTests
{
    [Fact]
    public void Exception_should_return_correct_object()
    {
        var exception = new SwedishSocialSecurityNumberException("Test error");

        exception.Message.Should().Be("Test error");
    }
}