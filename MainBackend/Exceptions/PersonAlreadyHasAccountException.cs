namespace MainBackend.Exceptions;

public class PersonAlreadyHasAccountException : Exception
{
    public PersonAlreadyHasAccountException(string message) : base(message)
    {
    }
}