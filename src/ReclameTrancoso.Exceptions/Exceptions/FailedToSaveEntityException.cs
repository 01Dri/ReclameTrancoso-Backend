namespace ReclameTrancoso.Exceptions.Exceptions;

public class FailedToSaveEntityException : Exception
{
    public FailedToSaveEntityException(string msg)
    :base(msg)
    {
        
    }
}