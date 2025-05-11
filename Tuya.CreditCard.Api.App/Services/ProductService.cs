using AutoMapper;
using Tuya.CreditCard.Api.App.Contracts.Services;
using Tuya.CreditCard.Api.Common.Contracts;
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
        private readonly IHttpHelperService _httpHelperService;

        public ProductService(IProductRepository productRepository, IMapper mapper, IHttpHelperService httpHelperService)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _httpHelperService = httpHelperService;
        }

        public async Task<List<Product>> GetAll()
        {
            var data = await _productRepository.GetAllAsync();
            ValidateObjectHelper<ProductEntity>.ValidateObjectList(data, true, $"No se encontraron datos", new KeyNotFoundException(string.Empty));
            var productList = _mapper.Map<List<Product>>(data);
            

            foreach (var item in productList)
            {
                if (!string.IsNullOrWhiteSpace(item.ImageUrl))
                    item.ImageUrl = GetProductImageUrl(item.ImageUrl);
            }

            return productList;
        }

        public string GetProductImageUrl(string imageName)
        {
            var baseUrl = _httpHelperService.GetBasePath();
            return $"{baseUrl}/images/{imageName}";
        }
    }
}
