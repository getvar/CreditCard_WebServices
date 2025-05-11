using AutoMapper;
using Tuya.CreditCard.Api.App.Contracts.Services;
using Tuya.CreditCard.Api.Common.Contracts;
using Tuya.CreditCard.Api.Common.Exceptions;
using Tuya.CreditCard.Api.Common.Helpers;
using Tuya.CreditCard.Api.DAL.Contracts.Entities;
using Tuya.CreditCard.Api.DAL.Contracts.Repositories;
using Tuya.CreditCard.Api.DAL.Mappers;
using Tuya.CreditCard.Api.DAL.Repositories;
using Tuya.CreditCard.Api.DTO.Models;

namespace Tuya.CreditCard.Api.App.Services
{
    public class SaleService : ISaleService
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IMapper _mapper;
        private readonly IApiAccessorUserData _apiAccessorUserData;
        private readonly ICardService _cardService;
        private readonly IProductService _productService;

        public SaleService(ISaleRepository saleRepository, IMapper mapper, IApiAccessorUserData apiAccessorUserData
               , ICardService cardService, IProductService productService)
        {
            _saleRepository = saleRepository;
            _mapper = mapper;
            _apiAccessorUserData = apiAccessorUserData;
            _cardService = cardService;
            _productService = productService;
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

            var createdSale = await _saleRepository.AddAsync(saveEntity);
            ValidateObjectHelper<SaleEntity>.ValidateObject(createdSale, true, $"{baseErrorMessage} Por favor, Intente más tarde", new NotInsertedException(string.Empty));
            return _mapper.Map<Sale>(await _saleRepository.GetByIdAsync(createdSale!.Id));
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
