using Tuya.CreditCard.Api.Common.Exceptions;

namespace Tuya.CreditCard.Api.Common.Helpers
{
    public static class ExceptionHelper
    {
        public static Exception GenerateException(string message, Exception? exception)
        {
            Exception resException = exception switch
            {
                NotInsertedException => new NotInsertedException(message),
                ArgumentException => new ArgumentException(message),
                KeyNotFoundException => new KeyNotFoundException(message),
                _ => new Exception(message)
            };

            throw resException;
        }
    }
}
