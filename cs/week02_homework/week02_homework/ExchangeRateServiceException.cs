namespace week02_homework;

public class ExchangeRateServiceException:Exception
{
    public ExchangeRateServiceException(Exception ex) : base(ex.Message, ex)
    {
        
    }
}