using AutoMapper;
using Tuya.CreditCard.Api.DAL.Contracts.Entities;
using Tuya.CreditCard.Api.DTO.Models;

namespace Tuya.CreditCard.Api.DAL.Mappers
{
    public static class CardMapper
    {
        public static CardEntity MapAdd(CardAdd card, IMapper mapper)
        {
            var entity = mapper.Map<CardEntity>(card);
            entity.Id = Guid.NewGuid();
            entity.RegistrationDate = DateTime.UtcNow;
            entity.UpdateDate = DateTime.UtcNow;
            entity.Last4Digits = card.CardNumber.Substring(card.CardNumber.Length - 4, 4);
            return entity;
        }

        public static CardEntity MapUpdate(CardEdit card, IMapper mapper)
        {
            var entity = mapper.Map<CardEntity>(card);
            entity.UpdateDate = DateTime.UtcNow;
            entity.Alias = card.Alias!;
            entity.OwnerEmail = card.OwnerEmail;
            entity.OwnerPhone = card.OwnerPhone;
            entity.OwnerName = card.OwnerName;
            return entity;
        }
    }
}
