using static Tuya.CreditCard.Api.DTO.Models.Enums;

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

    public class TransactionAdd
    {
        public Guid CardId { get; set; }

        public string TransactionReference { get; set; } = string.Empty;

        public decimal Value { get; set; }

        public DateTime CreationDate { get; set; }

        public TransactionState State { get; set; }

        public string? ResponseMessage { get; set; }
    }

    public class TransactionPaymentAdd
    {
        public Guid CardId { get; set; }
        public decimal Value { get; set; }
    }

    public class TransactionSend
    {
        public Guid CardId { get; set; }
        public string TransactionReference { get; set; } = string.Empty;
        public string CardToken { get; set; } = string.Empty;
        public decimal Value { get; set; }
        public string OwnerIdentificationType { get; set; } = string.Empty;
        public string OwnerIdentification { get; set; } = string.Empty;
        public string OwnerName { get; set; } = string.Empty;
    }

    public class TransactionResponse
    {
        public TransactionState State { get; set; }

        public string? ResponseMessage { get; set; }
    }
}
