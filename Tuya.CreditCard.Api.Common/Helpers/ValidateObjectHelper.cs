﻿namespace Tuya.CreditCard.Api.Common.Helpers
{
    public static class ValidateObjectHelper<T> where T : class
    {
        public static bool ValidateObject(T? element, bool generateException, string errorMessage, Exception? exception = null)
        {
            if (element != null)
                return true;

            if (generateException) ExceptionHelper.GenerateException(errorMessage, exception);
            return false;
        }

        public static bool ValidateObjectList(List<T>? list, bool generateException, string errorMessage, Exception? exception = null)
        {
            if (list?.Count > 0)
                return true;

            if (generateException) ExceptionHelper.GenerateException(errorMessage, exception);
            return false;
        }
    }
}
