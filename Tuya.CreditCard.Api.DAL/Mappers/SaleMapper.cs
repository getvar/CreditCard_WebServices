using AutoMapper;
using Tuya.CreditCard.Api.DAL.Contracts.Entities;
using Tuya.CreditCard.Api.DTO.Models;

namespace Tuya.CreditCard.Api.DAL.Mappers
{
    public static class SaleMapper
    {
        public static SaleEntity Map(SaleAdd entity, List<Product> products, IMapper mapper)
        {
            var saleToConfirm = mapper.Map<SaleEntity>(entity);
            saleToConfirm.Id = Guid.NewGuid();
            saleToConfirm.SaleCode = saleToConfirm.Id.ToString("N");
            saleToConfirm.CreationDate = DateTime.UtcNow;
            saleToConfirm.State = Enums.SaleState.Paid;
            saleToConfirm.SaleDetails = MapDetail(entity.SaleDetails, products, mapper);
            saleToConfirm.TotalValue = saleToConfirm.SaleDetails.Sum(x => x.TotalValue);
            return saleToConfirm;
        }

        private static List<SaleDetailEntity> MapDetail(List<SaleDetailAdd> details, List<Product> products, IMapper mapper)
        {
            var groupedDetails = details
                .GroupBy(d => d.ProductId)
                .Select(g => new SaleDetailAdd
                {
                    ProductId = g.Key,
                    Quantity = g.Sum(x => x.Quantity)
                })
                .ToList();

            var detailList = mapper.Map<List<SaleDetailEntity>>(groupedDetails);

            foreach (var item in detailList)
            {
                item.Id = Guid.NewGuid();
                item.UnitValue = products.Find(x => x.Id.Equals(item.ProductId))?.Price ?? 0;
                item.TotalValue = (item.UnitValue * item.Quantity);
            }

            return detailList;
        }
    }
}
