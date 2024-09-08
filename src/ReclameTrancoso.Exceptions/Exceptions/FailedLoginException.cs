namespace ReclameTrancoso.Exceptions.Exceptions;

public class FailedLoginException : Exception
{
    public FailedLoginException(string msg)
    :base(msg)
    {
        
    }
}