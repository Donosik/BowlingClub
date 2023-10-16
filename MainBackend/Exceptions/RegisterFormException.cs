namespace MainBackend.Exceptions;

public class RegisterFormException : Exception
{
    public RegisterFormException(string message) : base(message)
    {
    }
}