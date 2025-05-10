using Tuya.CreditCard.Api.DAL.Contracts.Entities;

namespace Tuya.CreditCard.Api.DAL.Contracts.Repositories
{
    public interface ICardRepository : IAdd<CardEntity>, IDelete<CardEntity>, IGetById<CardEntity>, IGetAllByUserId<CardEntity>
    {
    }
}
