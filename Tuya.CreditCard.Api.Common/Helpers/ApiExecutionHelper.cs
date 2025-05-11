using Tuya.CreditCard.Api.DTO.Models;

namespace Tuya.CreditCard.Api.Common.Helpers
{
    public static class ApiExecutionHelper
    {
        public static async Task<ApiResponse<T>> RunAsync<T>(Task<T> method)
        {
            var result = new ApiResponse<T>();
            try
            {
                result.Data = await method;
                result.SetState();
                result.SetMessage();
            }
            catch (Exception error)
            {
                throw error;
            }
            return result;
        }
    }
}
