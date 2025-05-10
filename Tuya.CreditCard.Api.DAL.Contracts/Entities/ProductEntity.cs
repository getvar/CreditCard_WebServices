using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tuya.CreditCard.Api.DAL.Contracts.Entities
{
    public class ProductEntity
    {
        public Guid Id { get; set; }

        [StringLength(100)]
        public string Reference { get; set; } = string.Empty;

        [StringLength(200)]
        public string Name { get; set; } = string.Empty;

        [StringLength(1000)]
        public string Description { get; set; } = string.Empty;

        [Column(TypeName = "numeric(18,2)")]
        public decimal Price { get; set; }

        [StringLength(500)]
        public string? ImageUrl { get; set; }

        public int AvailableQuantity { get; set; }

        public virtual ICollection<SaleDetailEntity> SaleDetails { get; set; } = null!;
    }
}
