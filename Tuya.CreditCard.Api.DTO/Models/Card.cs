using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using static Tuya.CreditCard.Api.DTO.Models.Enums;
using System.Text.Json.Serialization;

namespace Tuya.CreditCard.Api.DTO.Models
{
    public class Card
    {
        public Guid Id { get; set; }

        public string Last4Digits { get; set; } = string.Empty;

        public string Franchise { get; set; } = string.Empty;

        public string Bank { get; set; } = string.Empty;

        public string ExpirationDate { get; set; } = string.Empty;

        public string OwnerIdentificationType { get; set; } = string.Empty;

        public string OwnerIdentification { get; set; } = string.Empty;

        public string OwnerName { get; set; } = string.Empty;

        public string OwnerEmail { get; set; } = string.Empty;

        public string OwnerPhone { get; set; } = string.Empty;

        public string Alias { get; set; } = string.Empty;

        [JsonIgnore]
        public string Token { get; set; } = string.Empty;
    }

    public class CardAdd
    {
        [Required(ErrorMessage = "El NÚMERO DE LA TARJETA es obligatorio")]
        [RegularExpression(@"[0-9]+", ErrorMessage = "Debe enviar solo números en el NÚMERO DE LA TARJETA")]
        [StringLength(16, ErrorMessage = "El NÚMERO DE LA TARJETA no es válido")]
        public string CardNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = "El CÓDIGO DE SEGURIDAD es obligatorio")]
        [StringLength(3, ErrorMessage = "El CÓDIGO DE SEGURIDAD no es válido")]
        public string SecurityCode { get; set; } = string.Empty;

        [Required(ErrorMessage = "La FECHA DE VENCIMIENTO es obligatoria")]
        [Column(TypeName = "date")]
        public DateTime ExpirationDate { get; set; }

        [Required(ErrorMessage = "El TIPO DE IDENTIFICACIÓN es obligatorio")]
        public IdentificationType OwnerIdentificationType { get; set; }

        [Required(ErrorMessage = "La IDENTIFICACIÓN es obligatoria")]
        [StringLength(50, ErrorMessage = "La IDENTIFICACIÓN no puede exceder los 50 caracteres")]
        public string OwnerIdentification { get; set; } = string.Empty;

        [Required(ErrorMessage = "El NOMBRE DEL TITULAR es obligatorio")]
        [StringLength(300, ErrorMessage = "El NOMBRE DEL TITULAR no puede exceder los 300 caracteres")]
        public string OwnerName { get; set; } = string.Empty;

        [Required(ErrorMessage = "El EMAIL DEL TITULAR es obligatorio")]
        [RegularExpression(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$", ErrorMessage = "Debe enviar un EMAIL válido en el campo EMAIL DEL TITULAR")]
        [StringLength(300, ErrorMessage = "El EMAIL DEL TITULAR no puede exceder los 300 caracteres")]
        public string OwnerEmail { get; set; } = string.Empty;

        [Required(ErrorMessage = "El TELÉFONO DEL TITULAR es obligatorio")]
        [StringLength(50, ErrorMessage = "El TELÉFONO DEL TITULAR no puede exceder los 50 caracteres")]
        public string OwnerPhone { get; set; } = string.Empty;
    }

    public class CardEdit
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "El NOMBRE DEL TITULAR es obligatorio")]
        [StringLength(300, ErrorMessage = "El NOMBRE DEL TITULAR no puede exceder los 300 caracteres")]
        public string OwnerName { get; set; } = string.Empty;

        [Required(ErrorMessage = "El EMAIL DEL TITULAR es obligatorio")]
        [RegularExpression(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$", ErrorMessage = "Debe enviar un EMAIL válido en el campo EMAIL DEL TITULAR")]
        [StringLength(300, ErrorMessage = "El EMAIL DEL TITULAR no puede exceder los 300 caracteres")]
        public string OwnerEmail { get; set; } = string.Empty;

        [Required(ErrorMessage = "El TELÉFONO DEL TITULAR es obligatorio")]
        [StringLength(50, ErrorMessage = "El TELÉFONO DEL TITULAR no puede exceder los 50 caracteres")]
        public string OwnerPhone { get; set; } = string.Empty;

        [Required(ErrorMessage = "El ALIAS es obligatorio")]
        [StringLength(50, ErrorMessage = "El ALIAS no puede exceder los 50 caracteres")]
        public string Alias { get; set; } = string.Empty;

    }

    public class TokenizedCard
    {
        public string Token { get; set; } = string.Empty;
        public string Bank { get; set; } = string.Empty;
        public string Franchise { get; set; } = string.Empty;
    }

    public class CardTokenData
    {
        public string CardNumber { get; set; } = string.Empty;

        public string SecurityCode { get; set; } = string.Empty;

        public DateTime ExpirationDate { get; set; }

        public string OwnerName { get; set; } = string.Empty;
        
        public string OwnerIdentificationType { get; set; } = string.Empty;

        public string OwnerIdentification { get; set; } = string.Empty;
    }
}
