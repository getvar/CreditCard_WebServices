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

        public async Task<CardEntity?> DeleteAsync(Guid id)
        {
            var element = await GetByIdAsync(id);

            if (element != null)
            {
                element.UpdateDate = DateTime.UtcNow;
                element.State = Enums.CardState.Deleted;
                _creditCardContext.Cards.Update(element);
                return await _creditCardContext.SaveChangesAsync() > 0 ? element : null;
            }

            return null;
        }

        public async Task<CardEntity?> GetByIdAsync(Guid id) => await _creditCardContext.Cards.FirstOrDefaultAsync(x => x.Id.Equals(id) && x.State.Equals(CardState.Active));

        public async Task<List<CardEntity>> GetAllByUserIdAsync(Guid userId) => await _creditCardContext.Cards.Where(x => x.UserId.Equals(userId) && x.State.Equals(CardState.Active)).ToListAsync();

        public async Task<CardEntity?> GetCardByUserIdAndCardId(Guid userId, Guid cardId) => await _creditCardContext.Cards.FirstOrDefaultAsync(x => x.UserId.Equals(userId) && x.Id.Equals(cardId) && x.State.Equals(CardState.Active));

        public async Task<CardEntity?> EditAsync(CardEntity entity)
        {
            var element = await GetByIdAsync(entity.Id);

            if (element != null)
            {
                element.UpdateDate = entity.UpdateDate;
                element.Alias = entity.Alias;
                element.OwnerEmail = entity.OwnerEmail;
                element.OwnerName = entity.OwnerName;
                element.OwnerPhone = entity.OwnerPhone;
                _creditCardContext.Cards.Update(element);
                return await _creditCardContext.SaveChangesAsync() > 0 ? element : null;
            }

            return null;
        }
    }
}
