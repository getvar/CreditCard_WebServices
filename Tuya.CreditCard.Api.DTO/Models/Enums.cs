using System.ComponentModel.DataAnnotations;

namespace Tuya.CreditCard.Api.DTO.Models
{
    public class Enums
    {
        public enum CardState
        {
            [Display(Name = "Activa")]
            Active,
            [Display(Name = "Bloqueada")]
            Locked,
            [Display(Name = "Eliminada")]
            Deleted,
            [Display(Name = "Expirada")]
            Expired
        }

        public enum SaleState
        {
            [Display(Name = "Pendiente")]
            Pending,
            [Display(Name = "Pagada")]
            Paid,
            [Display(Name = "Cancelada")]
            Canceled
        }

        public enum TransactionState
        {
            [Display(Name = "Pendiente")]
            Pending,
            [Display(Name = "Ok")]
            Ok,
            [Display(Name = "Rechazada")]
            Rejected
        }
    }
}
