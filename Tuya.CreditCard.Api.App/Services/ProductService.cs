using AutoMapper;
using Tuya.CreditCard.Api.App.Contracts.Services;
using Tuya.CreditCard.Api.Common.Helpers;
using Tuya.CreditCard.Api.DAL.Contracts.Entities;
using Tuya.CreditCard.Api.DAL.Contracts.Repositories;
using Tuya.CreditCard.Api.DTO.Models;

namespace Tuya.CreditCard.Api.App.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<List<Product>> GetAll()
        {
            var data = await _productRepository.GetAllAsync();
            ValidateObjectHelper<ProductEntity>.ValidateObjectList(data, true, $"No se encontraron datos", new KeyNotFoundException(string.Empty));
            return _mapper.Map<List<Product>>(data);
        }
    }
}
