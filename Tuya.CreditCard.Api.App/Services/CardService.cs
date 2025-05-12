using AutoMapper;
using Tuya.CreditCard.Api.App.Contracts.Services;
using Tuya.CreditCard.Api.Common.Contracts;
using Tuya.CreditCard.Api.Common.Exceptions;
using Tuya.CreditCard.Api.Common.Helpers;
using Tuya.CreditCard.Api.DAL.Contracts.Entities;
using Tuya.CreditCard.Api.DAL.Contracts.Repositories;
using Tuya.CreditCard.Api.DAL.Mappers;
using Tuya.CreditCard.Api.DTO.Models;
using static Tuya.CreditCard.Api.DTO.Models.Enums;

namespace Tuya.CreditCard.Api.App.Services
{
    public class CardService : ICardService
    {
        private readonly ICardRepository _cardRepository;
        private readonly IMapper _mapper;
        private readonly IApiAccessorUserData _apiAccessorUserData;
        private readonly IPaymentService _paymentService;
        private readonly IMasterService _masterService;

        public CardService(ICardRepository cardRepository, IMapper mapper, IApiAccessorUserData apiAccessorUserData, IPaymentService paymentService, IMasterService masterService)
        {
            _cardRepository = cardRepository;
            _mapper = mapper;
            _apiAccessorUserData = apiAccessorUserData;
            _paymentService = paymentService;
            _masterService = masterService;
        }

        public async Task<bool> AddCard(CardAdd card)
        {
            string baseErrorMessage = "No fue posible crear la tarjeta.";
            await ValidateAddCardData(card, baseErrorMessage);
            var entity = CardMapper.MapAdd(card, _mapper);
            var tokenizedCard = await _paymentService.TokenizeCreditCard(new CardTokenData()
            {
                CardNumber = card.CardNumber,
                ExpirationDate = card.ExpirationDate,
                OwnerIdentification = card.OwnerIdentification,
                OwnerIdentificationType = card.OwnerIdentificationType.ToString(),
                OwnerName = card.OwnerName,
                SecurityCode = card.SecurityCode
            });
            ValidateObjectHelper<TokenizedCard>.ValidateObject(tokenizedCard, true, $"{baseErrorMessage} Por favor, Intente más tarde", new NotInsertedException(string.Empty));
            entity.Token = tokenizedCard.Token;
            entity.Bank = tokenizedCard.Bank;
            entity.Franchise = tokenizedCard.Franchise;
            entity.UserId = _apiAccessorUserData.GetUserId();
            entity.Alias = $"Tarjeta terminada en {entity.Last4Digits}";
            var createdUser = await _cardRepository.AddAsync(entity);
            ValidateObjectHelper<CardEntity>.ValidateObject(createdUser, true, $"{baseErrorMessage} Por favor, Intente más tarde", new NotInsertedException(string.Empty));
            return true;
        }

        public async Task<List<Card>> GetAllCards()
        {
            var data = await _cardRepository.GetAllByUserIdAsync(_apiAccessorUserData.GetUserId());
            ValidateObjectHelper<CardEntity>.ValidateObjectList(data, true, $"No se encontró información", new KeyNotFoundException(string.Empty));
            return _mapper.Map<List<Card>>(data);
        }

        public async Task<bool> UpdateCard(CardEdit card)
        {
            string baseErrorMessage = "No fue posible actualizar la tarjeta.";
            await ValidateUpdateCardData(card, baseErrorMessage);
            var entity = CardMapper.MapUpdate(card, _mapper);
            var updatedCard = await _cardRepository.EditAsync(entity);
            ValidateObjectHelper<CardEntity>.ValidateObject(updatedCard, true, $"{baseErrorMessage} Por favor, Intente más tarde", new NotInsertedException(string.Empty));
            return true;
        }

        public async Task<bool> DeleteCard(Guid cardId)
        {
            string baseErrorMessage = "No fue posible eliminar la tarjeta.";
            await ValidateExistsCardError(cardId, baseErrorMessage);
            var deletedCard = await _cardRepository.DeleteAsync(cardId);
            ValidateObjectHelper<CardEntity>.ValidateObject(deletedCard, true, $"{baseErrorMessage} Por favor, Intente más tarde", new NotInsertedException(string.Empty));
            return true;
        }

        public async Task<bool> ValidateExistsCard(Guid cardId)
        {
            var existsCard = await _cardRepository.GetCardByUserIdAndCardId(_apiAccessorUserData.GetUserId(), cardId);
            return existsCard != null;
        }

        private async Task ValidateAddCardData(CardAdd card, string baseErrorMessage)
        {
            if (_apiAccessorUserData.GetUserId().Equals(Guid.Empty))
                ExceptionHelper.GenerateException($"{baseErrorMessage} El usuario no ha iniciado sesión", new UnauthorizedException(string.Empty));

            if (!await _masterService.ExistsIdentificationType((int)card.OwnerIdentificationType))
                ExceptionHelper.GenerateException($"{baseErrorMessage} Debe enviar un TIPO DE IDENTIFICACIÓN válido", new ArgumentException(string.Empty));

            ValidationHelper.ValidateEmptyString(card.OwnerIdentification, true, $"{baseErrorMessage} La IDENTIFICACIÓN es obligatoria");
            ValidationHelper.ValidateEmptyString(card.CardNumber, true, $"{baseErrorMessage} El NÚMERO DE LA TARJETA es obligatorio");
            ValidationHelper.ValidateEmptyString(card.SecurityCode, true, $"{baseErrorMessage} El CÓDIGO DE SEGURIDAD es obligatorio");
            ValidationHelper.ValidateEmptyString(card.OwnerName, true, $"{baseErrorMessage} El NOMBRE DEL TITULAR es obligatorio");
            ValidationHelper.ValidateEmptyString(card.OwnerEmail, true, $"{baseErrorMessage} El EMAIL DEL TITULAR es obligatorio");
            ValidationHelper.ValidateEmptyString(card.OwnerPhone, true, $"{baseErrorMessage} El TELÉFONO DEL TITULAR es obligatorio");

            if (card.ExpirationDate.Year < DateTime.Now.Year 
                || (card.ExpirationDate.Year.Equals(DateTime.Now.Year) && card.ExpirationDate.Month < DateTime.Now.Month))
                ExceptionHelper.GenerateException($"{baseErrorMessage} La tarjeta está vencida", new ArgumentException(string.Empty));
        }

        private async Task ValidateUpdateCardData(CardEdit card, string baseErrorMessage)
        {
            if (_apiAccessorUserData.GetUserId().Equals(Guid.Empty))
                ExceptionHelper.GenerateException($"{baseErrorMessage} El usuario no ha iniciado sesión", new UnauthorizedException(string.Empty));

            if (card.Id == Guid.Empty)
                ExceptionHelper.GenerateException($"{baseErrorMessage} Debe enviar el ID de la tarjeta", new ArgumentException(string.Empty));

            await ValidateExistsCardError(card.Id, baseErrorMessage);
            ValidationHelper.ValidateEmptyString(card.Alias, true, $"{baseErrorMessage} El ALIAS DE LA TARJETA es obligatorio");
            ValidationHelper.ValidateEmptyString(card.OwnerName, true, $"{baseErrorMessage} El NOMBRE DEL TITULAR es obligatorio");
            ValidationHelper.ValidateEmptyString(card.OwnerEmail, true, $"{baseErrorMessage} El EMAIL DEL TITULAR es obligatorio");
            ValidationHelper.ValidateEmptyString(card.OwnerPhone, true, $"{baseErrorMessage} El TELÉFONO DEL TITULAR es obligatorio");
        }

        private async Task ValidateExistsCardError(Guid cardId, string baseErrorMessage)
        {
            var existsCard = await _cardRepository.GetCardByUserIdAndCardId(_apiAccessorUserData.GetUserId(), cardId);
            ValidateObjectHelper<CardEntity>.ValidateObject(existsCard, true, $"{baseErrorMessage} La tarjeta no existe o ya no está activa", new KeyNotFoundException(string.Empty));
        }

        public async Task<Card> GetCardById(Guid id) => _mapper.Map<Card>(await _cardRepository.GetByIdAsync(id));
    }
}
