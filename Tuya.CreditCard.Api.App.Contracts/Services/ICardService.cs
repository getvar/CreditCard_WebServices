using Tuya.CreditCard.Api.DTO.Models;

namespace Tuya.CreditCard.Api.App.Contracts.Services
{
    public interface ICardService
    {
        Task<bool> AddCard(CardAdd card);
        Task<List<Card>> GetAllCards();
        Task<bool> UpdateCard(CardEdit card);
        Task<bool> DeleteCard(Guid cardId);
        Task<bool> ValidateExistsCard(Guid cardId);
        Task<Card> GetCardById(Guid id);
    }
}
