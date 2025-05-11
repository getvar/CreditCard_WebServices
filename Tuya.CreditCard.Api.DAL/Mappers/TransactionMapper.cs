using Tuya.CreditCard.Api.DAL.Contracts.Entities;
using Tuya.CreditCard.Api.DTO.Models;

namespace Tuya.CreditCard.Api.DAL.Mappers
{
    public static class TransactionMapper
    {
        public static ICollection<TransactionEntity> Map(Transaction transaction, SaleEntity saveEntity, Card card)
        {
            List<TransactionEntity> response = new List<TransactionEntity>()
            {
                new TransactionEntity()
                {
                    CardId = card.Id,
                    CreationDate = DateTime.UtcNow,
                    Id = Guid.NewGuid(),
                    ResponseMessage = transaction.ResponseMessage,
                    State = Enums.TransactionState.Ok,
                    TransactionReference = transaction.TransactionReference,
                    Value = saveEntity.TotalValue,
                }
            };

            return response;
        }
    }
}
