namespace Tuya.CreditCard.Api.Common.Helpers
{
    public static class ValidationHelper
    {
        public static bool ValidateEmptyString(string? value, bool generateException, string errorMessage)
        {
            if (!string.IsNullOrWhiteSpace(value) && value.Trim().Length > 0)
                return true;

            if (generateException) ExceptionHelper.GenerateException(errorMessage, new ArgumentException(errorMessage));
            return false;
        }
    }
}
