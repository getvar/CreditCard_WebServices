﻿using Tuya.CreditCard.Api.DTO.Models;

namespace Tuya.CreditCard.Api.App.Contracts.Services
{
    public interface IProductService
    {
        Task<List<Product>> GetAll();
        string GetProductImageUrl(string imageName);
    }
}
