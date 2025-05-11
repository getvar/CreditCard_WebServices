namespace Tuya.CreditCard.Api.DTO.Models
{
    public class ApiResponse<T>
    {
        public ApiResponse()
        {
            Success = false;
        }

        public bool Success { get; set; }
        public string? Message { get; set; }
        public T? Data { get; set; }

        public void SetState()
        {
            Success = Data != null;
        }

        public void LogError(Exception error)
        {
            Message = error?.Message;
        }

        public void SetMessage()
        {
            Message = "Acción exitosa";
        }
    }
}
