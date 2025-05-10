using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static Tuya.CreditCard.Api.DTO.Models.Enums;

namespace Tuya.CreditCard.Api.DAL.Contracts.Entities
{
    public class CardEntity
    {
        public Guid Id { get; set; }

        [StringLength(100)]
        public string Alias { get; set; } = string.Empty;

        [StringLength(4)]
        public string Last4Digits { get; set; } = string.Empty;

        [StringLength(70)]
        public string Franchise { get; set; } = string.Empty;

        [Column(TypeName = "date")]
        public DateTime ExpirationDate { get; set; }

        [StringLength(100)]
        public string Token { get; set; } = string.Empty;

        public bool Default { get; set; }

        public CardState State { get; set; } = CardState.Active;

        [Column(TypeName = "datetime")]
        public DateTime RegistrationDate { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime? UpdateDate { get; set; }

        public virtual CardEntity Card { get; set; } = new CardEntity();

        public virtual ICollection<TransactionEntity> Transactions { get; set; } = null!;
    }
}
