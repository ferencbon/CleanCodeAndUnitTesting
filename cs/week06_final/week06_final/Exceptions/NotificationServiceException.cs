namespace week06_final.Exceptions;

public class NotificationServiceException : Exception
{
    public NotificationServiceException(string message, Exception exception) : base(message, exception)
    {
    }
}