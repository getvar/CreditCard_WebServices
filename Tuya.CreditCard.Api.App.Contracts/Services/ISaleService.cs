using Tuya.CreditCard.Api.DTO.Models;

namespace Tuya.CreditCard.Api.App.Contracts.Services
{
    public interface ISaleService
    {
        Task<Sale> ConfirmSale(SaleAdd entity);
    }
}
