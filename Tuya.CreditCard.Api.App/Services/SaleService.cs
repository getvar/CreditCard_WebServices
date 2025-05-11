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
    public class SaleService : ISaleService
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IMapper _mapper;
        private readonly IApiAccessorUserData _apiAccessorUserData;
        private readonly ICardService _cardService;
        private readonly IProductService _productService;
        private readonly ITransactionService _transactionService;

        public SaleService(ISaleRepository saleRepository, IMapper mapper, IApiAccessorUserData apiAccessorUserData
            , ICardService cardService, IProductService productService, ITransactionService transactionService)
        {
            _saleRepository = saleRepository;
            _mapper = mapper;
            _apiAccessorUserData = apiAccessorUserData;
            _cardService = cardService;
            _productService = productService;
            _transactionService = transactionService;
        }

        public async Task<Sale> ConfirmSale(SaleAdd entity)
        {
            string baseErrorMessage = "No fue posible confirmar la compra.";
            await ValidateSaleData(entity, baseErrorMessage);
            var products = await _productService.GetAll();
            var saveEntity = SaleMapper.Map(entity, products, _mapper);
            saveEntity.UserId = _apiAccessorUserData.GetUserId();

            if (saveEntity.SaleDetails.Any(x => x.TotalValue <= 0))
                ExceptionHelper.GenerateException($"{baseErrorMessage} Al menos un producto no está disponible", new ArgumentException(string.Empty));

            var transaction = await _transactionService.ConfirmTransaction(new TransactionPaymentAdd()
            {
                CardId = entity.CardId,
                Value = saveEntity.TotalValue
            });

            if (transaction == null || !transaction.State.Equals(TransactionState.Ok))
                ExceptionHelper.GenerateException($"{baseErrorMessage} No fue posible realizar el pago", new ArgumentException(string.Empty));

            saveEntity.Transactions = new List<TransactionEntity>
            {
                _mapper.Map<TransactionEntity>(transaction)
            };
            var createdSale = await _saleRepository.AddAsync(saveEntity);
            ValidateObjectHelper<SaleEntity>.ValidateObject(createdSale, true, $"{baseErrorMessage} Por favor, Intente más tarde", new NotInsertedException(string.Empty));
            var sale = await _saleRepository.GetByIdAsync(createdSale!.Id);
            sale!.SaleDetails.ToList().ForEach(item =>
            {
                item.Product.ImageUrl = _productService.GetProductImageUrl(item.Product.ImageUrl!);
            });
            return _mapper.Map<Sale>(sale);
        }

        public async Task<List<Sale>> GetSaleListByUserId()
        {
            var saleList = await _saleRepository.GetAllByUserIdAsync(_apiAccessorUserData.GetUserId());
            ValidateObjectHelper<SaleEntity>.ValidateObjectList(saleList, true, $"No se encontró información", new KeyNotFoundException(string.Empty));
            saleList.SelectMany(sale => sale.SaleDetails).ToList().ForEach(item =>
                item.Product.ImageUrl = _productService.GetProductImageUrl(item.Product.ImageUrl!)
            );
            return _mapper.Map<List<Sale>>(saleList);
        }

        private async Task ValidateSaleData(SaleAdd entity, string baseErrorMessage)
        {
            if (entity.CardId == Guid.Empty || !await _cardService.ValidateExistsCard(entity.CardId))
                ExceptionHelper.GenerateException($"{baseErrorMessage} Debe enviar una tarjeta válida", new ArgumentException(string.Empty));

            ValidateObjectHelper<SaleDetailAdd>.ValidateObjectList(entity.SaleDetails, true, $"{baseErrorMessage} Debe enviar al menos un producto", new ArgumentException(string.Empty));

            if (entity.SaleDetails.Exists(x => x.ProductId.Equals(Guid.Empty) || x.Quantity <= 0))
                ExceptionHelper.GenerateException($"{baseErrorMessage} Debe enviar los productos con cantidades válidas", new ArgumentException(string.Empty));

            var products = await _productService.GetAll();
            ValidateObjectHelper<Product>.ValidateObjectList(products, true, $"{baseErrorMessage} No hay productos disponibles", new KeyNotFoundException(string.Empty));

            if (entity.SaleDetails.Exists(sd => !products.Exists(x => x.Id.Equals(sd.ProductId))))
                ExceptionHelper.GenerateException($"{baseErrorMessage} Debe enviar productos válidos", new ArgumentException(string.Empty));
        }
    }
}
