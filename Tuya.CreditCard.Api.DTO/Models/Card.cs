using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Tuya.CreditCard.Api.DTO.Models
{
    public class Card
    {
        public Guid Id { get; set; }

        public string Last4Digits { get; set; } = string.Empty;

        public string Franchise { get; set; } = string.Empty;

        public string Bank { get; set; } = string.Empty;

        public string ExpirationDate { get; set; } = string.Empty;
    }

    public class CardAdd
    {
        [Required(ErrorMessage = "Alias is mandatory")]
        [StringLength(50, ErrorMessage = "Alias cannot exceed 50 characters")]
        public string Alias { get; set; } = string.Empty;

        [Required(ErrorMessage = "Bank is mandatory")]
        [StringLength(200, ErrorMessage = "Bank cannot exceed 200 characters")]
        public string Bank { get; set; } = string.Empty;

        [Required(ErrorMessage = "Franchise is mandatory")]
        [StringLength(70, ErrorMessage = "Franchise cannot exceed 70 characters")]
        public string Franchise { get; set; } = string.Empty;

        [Required(ErrorMessage = "CardNumber is mandatory")]
        [StringLength(100, ErrorMessage = "CardNumber cannot exceed 100 characters")]
        public string CardNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = "SecurityCode is mandatory")]
        [StringLength(100, ErrorMessage = "SecurityCode cannot exceed 100 characters")]
        public string SecurityCode { get; set; } = string.Empty;

        [Required(ErrorMessage = "ExpirationDate is mandatory")]
        [Column(TypeName = "date")]
        public DateTime ExpirationDate { get; set; }
    }
}
