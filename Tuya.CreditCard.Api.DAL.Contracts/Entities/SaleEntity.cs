using System.ComponentModel.DataAnnotations.Schema;
using static Tuya.CreditCard.Api.DTO.Models.Enums;

namespace Tuya.CreditCard.Api.DAL.Contracts.Entities
{
    public class SaleEntity
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public string SaleCode { get; set; } = string.Empty;

        [Column(TypeName = "datetime")]
        public DateTime CreationDate { get; set; }

        [Column(TypeName = "numeric(18,2)")]
        public decimal TotalValue { get; set; }

        public SaleState State { get; set; } = SaleState.Pending;

        public virtual UserEntity User { get; set; } = null!;

        public virtual ICollection<SaleDetailEntity> SaleDetails { get; set; } = null!;

        public virtual ICollection<TransactionEntity> Transactions { get; set; } = null!;
    }
}
