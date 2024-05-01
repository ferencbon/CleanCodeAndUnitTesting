namespace week06_final.Exceptions;

public class RepositoryException : Exception
{
    public RepositoryException(string message, Exception innerException) : base(message, innerException)
    {
    }
}