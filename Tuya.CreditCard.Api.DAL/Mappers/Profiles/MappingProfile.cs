using AutoMapper;
using Tuya.CreditCard.Api.Common.Extensions;
using Tuya.CreditCard.Api.DAL.Contracts.Entities;
using Tuya.CreditCard.Api.DTO.Models;

namespace Tuya.CreditCard.Api.DAL.Mappers.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserManage, UserEntity>();

            CreateMap<UserEntity, UserManage>();

            CreateMap<UserData, User>();

            CreateMap<UserEntity, User>()
                .ForMember(target => target.IdentificationType, opt => opt.MapFrom(src => src.IdentificationType.GetDisplayName()));

            CreateMap<UserEntity, UserData>()
                .ForMember(target => target.FullName, opt => opt.MapFrom(src => $"{src.Name} {src.LastName}"));

            CreateMap<CardAdd, CardEntity>();

            CreateMap<CardEdit, CardEntity>();

            CreateMap<CardEntity, Card>()
                .ForMember(target => target.ExpirationDate, opt => opt.MapFrom(src => src.ExpirationDate.ToString("MM/yyyy")));

            CreateMap<ProductEntity, Product>();

            CreateMap<SaleAdd, SaleEntity>();

            CreateMap<SaleEntity, Sale>()
                .ForMember(target => target.CreationDate, opt => opt.MapFrom(src => src.CreationDate.ToString("dd/MM/yyyy HH:mm")))
                .ForMember(dest => dest.TotalValue, opt => opt.MapFrom(src => $"${src.TotalValue:N0}"))
                .ForMember(dest => dest.ProductQuantity, opt => opt.MapFrom(src => src.SaleDetails.Count))
                .ForMember(dest => dest.CardAlias, opt => opt.MapFrom(src => src.Transactions.First().Card.Alias))
                .ForMember(dest => dest.State, opt => opt.MapFrom(src => src.State.GetDisplayName()))
                .ForMember(dest => dest.SaleDetails, opt => opt.MapFrom(src => src.SaleDetails));

            CreateMap<SaleDetailAdd, SaleDetailEntity>();

            CreateMap<SaleDetailEntity, SaleDetail>()
                .ForMember(target => target.ProductName, opt => opt.MapFrom(src => src.Product.Name))
                .ForMember(dest => dest.ProductImageUrl, opt => opt.MapFrom(src => src.Product.ImageUrl))
                .ForMember(dest => dest.UnitValue, opt => opt.MapFrom(src => $"${src.UnitValue:N0}"))
                .ForMember(dest => dest.TotalValue, opt => opt.MapFrom(src => $"${src.TotalValue:N0}"));

            CreateMap<TransactionEntity, Transaction>()
                .ForMember(target => target.SaleCode, opt => opt.MapFrom(src => src.Sale.SaleCode))
                .ForMember(target => target.CardAlias, opt => opt.MapFrom(src => src.Card.Alias))
                .ForMember(dest => dest.Value, opt => opt.MapFrom(src => $"${src.Value:N0}"))
                .ForMember(target => target.CreationDate, opt => opt.MapFrom(src => src.CreationDate.ToString("dd/MM/yyyy HH:mm")))
                .ForMember(dest => dest.State, opt => opt.MapFrom(src => src.State.GetDisplayName()));
        }
    }
}
