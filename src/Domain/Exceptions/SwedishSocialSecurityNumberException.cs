namespace Domain.Exceptions;

public class SwedishSocialSecurityNumberException : Exception
{
    internal SwedishSocialSecurityNumberException(string message) : base(message) { }
}