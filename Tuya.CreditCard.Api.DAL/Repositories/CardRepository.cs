using Microsoft.EntityFrameworkCore;
using Tuya.CreditCard.Api.DAL.Contracts;
using Tuya.CreditCard.Api.DAL.Contracts.Entities;
using Tuya.CreditCard.Api.DAL.Contracts.Repositories;
using Tuya.CreditCard.Api.DTO.Models;
using static Tuya.CreditCard.Api.DTO.Models.Enums;

namespace Tuya.CreditCard.Api.DAL.Repositories
{
    public class CardRepository : ICardRepository
    {
        private readonly ICreditCardContext _creditCardContext;

        public CardRepository(ICreditCardContext creditCardContext)
        {
            _creditCardContext = creditCardContext;
        }

        public async Task<CardEntity?> AddAsync(CardEntity entity)
        {
            _creditCardContext.Cards.Add(entity);
            return await _creditCardContext.SaveChangesAsync() > 0 ? entity : null;
        }

        public async Task<CardEntity?> DeleteAsync(CardEntity entity)
        {
            var element = await GetByIdAsync(entity.Id);

            if (element != null)
            {
                element.UpdateDate = DateTime.UtcNow;
                element.State = Enums.CardState.Deleted;
                _creditCardContext.Cards.Update(element);
                return await _creditCardContext.SaveChangesAsync() > 0 ? element : null;
            }

            return null;
        }

        public async Task<CardEntity?> GetByIdAsync(Guid id) => await _creditCardContext.Cards.FindAsync(id);

        public async Task<List<CardEntity>> GetAllAsync(Guid id) => await _creditCardContext.Cards.Where(x => x.State.Equals(CardState.Active)).ToListAsync();

        public async Task<List<CardEntity>> GetAllByUserIdAsync(Guid userId) => await _creditCardContext.Cards.Where(x => x.UserId.Equals(userId) && x.State.Equals(CardState.Active)).ToListAsync();
    }
}
