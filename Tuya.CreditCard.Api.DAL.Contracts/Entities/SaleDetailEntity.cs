using System.ComponentModel.DataAnnotations.Schema;

namespace Tuya.CreditCard.Api.DAL.Contracts.Entities
{
    public class SaleDetailEntity
    {
        public Guid Id { get; set; }

        public Guid SaleId { get; set; }

        public Guid ProductId { get; set; }

        public int Quantity { get; set; }

        [Column(TypeName = "numeric(18,2)")]
        public decimal UnitValue { get; set; }

        [Column(TypeName = "numeric(18,2)")]
        public decimal TotalValue { get; set; }

        public virtual SaleEntity Sale { get; set; } = null!;

        public virtual ProductEntity Product { get; set; } = null!;
    }
}
