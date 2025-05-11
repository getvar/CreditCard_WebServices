using AutoMapper;
using Tuya.CreditCard.Api.App.Contracts.Services;
using Tuya.CreditCard.Api.Common.Contracts;
using Tuya.CreditCard.Api.Common.Helpers;
using Tuya.CreditCard.Api.DAL.Contracts.Entities;
using Tuya.CreditCard.Api.DAL.Contracts.Repositories;
using Tuya.CreditCard.Api.DTO.Models;
using static Tuya.CreditCard.Api.DTO.Models.Enums;

namespace Tuya.CreditCard.Api.App.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IPaymentService _paymentService;
        private readonly ICardService _cardService;
        private readonly IApiAccessorUserData _apiAccessorUserData;
        private readonly IMapper _mapper;

        public TransactionService(ITransactionRepository transactionRepository, IPaymentService paymentService, ICardService cardService
            , IApiAccessorUserData apiAccessorUserData, IMapper mapper)
        {
            _transactionRepository = transactionRepository;
            _paymentService = paymentService;
            _cardService = cardService;
            _apiAccessorUserData = apiAccessorUserData;
            _mapper = mapper;
        }

        public async Task<TransactionAdd?> ConfirmTransaction(TransactionPaymentAdd transactionEntity)
        {
            var card = await _cardService.GetCardById(transactionEntity.CardId);
            var transactionToPay = new TransactionSend()
            {
                TransactionReference = GenericHelper.GenerateGuidWithoutHyphen(),
                CardId = transactionEntity.CardId,
                Value = transactionEntity.Value,
                CardToken = card.Token,
                OwnerIdentification = card.OwnerIdentification,
                OwnerIdentificationType = card.OwnerIdentificationType,
                OwnerName = card.OwnerName
            };
            var transaction = await _paymentService.SendTransaction(transactionToPay);

            if (transaction != null && transaction.State.Equals(TransactionState.Ok))
            {
                return new TransactionAdd()
                {
                    TransactionReference = transactionToPay.TransactionReference,
                    Value = transactionEntity.Value,
                    State = transaction.State,
                    ResponseMessage = transaction.ResponseMessage,
                    CardId = transactionEntity.CardId,
                    CreationDate = DateTime.UtcNow,
                };
            }

            return null;
        }

        public async Task<List<Transaction>> GetConfirmTransactions()
        {
            var transactionList = await _transactionRepository.GetAllByUserIdAsync(_apiAccessorUserData.GetUserId());
            ValidateObjectHelper<TransactionEntity>.ValidateObjectList(transactionList, true, $"No se encontró información", new KeyNotFoundException(string.Empty));
            return _mapper.Map<List<Transaction>>(transactionList);
        }
    }
}
