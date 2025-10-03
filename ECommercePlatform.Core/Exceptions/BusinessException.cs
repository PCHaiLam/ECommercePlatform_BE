namespace ECommercePlatform.Core.Exceptions
{
    public class BusinessException : BaseException
    {
        public BusinessException(string message) : base(message, "BUSINESS_ERROR")
        {
        }
    }
    
    public class UnauthorizedException : BaseException
    {
        public UnauthorizedException(string message = "Unauthorized access") : base(message, "UNAUTHORIZED")
        {
        }
    }
}