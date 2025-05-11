namespace Tuya.CreditCard.Api.Common.Exceptions
{
    public class NotInsertedException : Exception
    {
        public NotInsertedException(string message) : base(message) { }
    }

    public class NotUpdatedException : Exception
    {
        public NotUpdatedException(string message) : base(message) { }
    }

    public class UnauthorizedException : Exception
    {
        public UnauthorizedException(string message) : base(message) { }
    }
}
