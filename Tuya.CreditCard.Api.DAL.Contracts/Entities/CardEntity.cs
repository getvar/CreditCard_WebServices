using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static Tuya.CreditCard.Api.DTO.Models.Enums;

namespace Tuya.CreditCard.Api.DAL.Contracts.Entities
{
    public class CardEntity
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        [StringLength(50)]
        public string Alias { get; set; } = string.Empty;

        [StringLength(200)]
        public string Bank { get; set; } = string.Empty;

        [StringLength(4)]
        public string Last4Digits { get; set; } = string.Empty;

        [StringLength(70)]
        public string Franchise { get; set; } = string.Empty;

        [Column(TypeName = "date")]
        public DateTime ExpirationDate { get; set; }

        [StringLength(100)]
        public string Token { get; set; } = string.Empty;

        public CardState State { get; set; } = CardState.Active;

        [Column(TypeName = "datetime")]
        public DateTime RegistrationDate { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime? UpdateDate { get; set; }

        public IdentificationType OwnerIdentificationType { get; set; }

        [StringLength(50)]
        public string OwnerIdentification { get; set; } = string.Empty;

        [StringLength(300)]
        public string OwnerName { get; set; } = string.Empty;

        [StringLength(300)]
        public string OwnerEmail { get; set; } = string.Empty;

        [StringLength(50)]
        public string OwnerPhone { get; set; } = string.Empty;

        public virtual UserEntity User { get; set; } = null!;

        public virtual ICollection<TransactionEntity> Transactions { get; set; } = null!;
    }
}
