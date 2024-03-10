using Domain.Exceptions;
using FluentAssertions;
using FluentAssertions.Execution;

namespace Domain.Tests.Exceptions;

public class SwedishCorporateIdExceptionTests
{
    [Fact]
    public void Empty_exception_should_return_correct_object()
    {
        var exception = new SwedishCorporateIdException();

        using (new AssertionScope())
        {
            exception.Message.Should().Be("Invalid Swedish organization number");
            exception.InnerException.Should().BeNull();
        }
    }
    
    [Fact]
    public void Exception_with_message_should_return_correct_object()
    {
        var exception = new SwedishCorporateIdException("Test error");

        using (new AssertionScope())
        {
            exception.Message.Should().Be("Test error");
            exception.InnerException.Should().BeNull();
        }
    }
    
    [Fact]
    public void Exception_with_inner_exception_should_return_correct_object()
    {
        var exception = new SwedishCorporateIdException("Test error", new Exception("Test error"));

        using (new AssertionScope())
        {
            exception.Message.Should().Be("Test error");
            exception.InnerException!.Message.Should().Be("Test error");
        }
    }
}