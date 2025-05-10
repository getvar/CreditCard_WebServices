namespace Tuya.CreditCard.Api.DTO.Models
{
    public class Transaction
    {
        public string TransactionReference { get; set; } = string.Empty;

        public string SaleCode { get; set; } = string.Empty;

        public string CardAlias { get; set; } = string.Empty;

        public string Value { get; set; } = string.Empty;

        public string CreationDate { get; set; } = string.Empty;

        public string State { get; set; } = string.Empty;

        public string? ResponseMessage { get; set; }
    }
}
