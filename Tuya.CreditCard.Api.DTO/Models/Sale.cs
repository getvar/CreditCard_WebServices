using System.ComponentModel.DataAnnotations.Schema;

namespace Tuya.CreditCard.Api.DTO.Models
{
    public class Sale
    {
        public Guid Id { get; set; }

        public string SaleCode { get; set; } = string.Empty;

        public string CreationDate { get; set; } = string.Empty;

        public string TotalValue { get; set; } = string.Empty;

        public string State { get; set; } = string.Empty;

        public int ProductQuantity { get; set; }

        public string CardAlias { get; set; } = string.Empty;

        public List<SaleDetail> SaleDetails { get; set; } = new List<SaleDetail>();
    }

    public class SaleDetail
    {
        public string ProductName { get; set; } = string.Empty;
        
        public string ProductImageUrl { get; set; } = string.Empty;

        public int Quantity { get; set; }

        public string UnitValue { get; set; } = string.Empty;

        public string TotalValue { get; set; } = string.Empty;
    }

    public class SaleAdd
    {
        public Guid CardId { get; set; }
        public decimal TotalValue { get; set; }
    }

    public class SaleDetailAdd
    {
        public Guid ProductId { get; set; }

        public int Quantity { get; set; }

        public decimal UnitValue { get; set; }
    }
}
