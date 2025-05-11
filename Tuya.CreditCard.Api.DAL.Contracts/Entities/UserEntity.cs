using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static Tuya.CreditCard.Api.DTO.Models.Enums;

namespace Tuya.CreditCard.Api.DAL.Contracts.Entities
{
    public class UserEntity
    {
        public Guid Id { get; set; }

        public IdentificationType IdentificationType { get; set; }

        [StringLength(50)]
        public string Identification { get; set; } = string.Empty;

        [StringLength(200)]
        public string Name { get; set; } = string.Empty;

        [StringLength(200)]
        public string LastName { get; set; } = string.Empty;

        [StringLength(50)]
        public string Phone { get; set; } = string.Empty;

        [StringLength(200)]
        public string Adrress { get; set; } = string.Empty;

        [StringLength(400)]
        public string UserName { get; set; } = string.Empty;

        [StringLength(500)]
        public string Password { get; set; } = string.Empty;

        [Column(TypeName = "datetime")]
        public DateTime RegistrationDate { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime UpdateDate { get; set; }

        public virtual ICollection<CardEntity> Cards { get; set; } = null!;
        public virtual ICollection<SaleEntity> Sales { get; set; } = null!;
    }
}
