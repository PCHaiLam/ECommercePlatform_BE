namespace ECommercePlatform.Core.Exceptions
{
    public class NotFoundException : BaseException
    {
        public NotFoundException(string message) : base(message, "NOT_FOUND")
        {
        }

        public NotFoundException(string resourceName, object resourceId) 
            : base($"{resourceName} with ID '{resourceId}' was not found", "NOT_FOUND")
        {
        }
    }
}
