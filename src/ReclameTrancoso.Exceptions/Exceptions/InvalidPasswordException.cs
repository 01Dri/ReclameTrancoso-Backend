namespace ReclameTrancoso.Exceptions.Exceptions;

public class InvalidPasswordException : Exception
{

    public InvalidPasswordException(string msg)
        : base(msg)
    {
    }
}
