using System;
using ee.itcollege.nikita.Contracts.DAL.Base.Mappers;

namespace DAL.App.EF.Mappers
{
    public class ProductSoldMapper : IBaseDALMapper
    {
        public TOutObject Map<TOutObject>(object inObject) where TOutObject : class
        {
            if (typeof(TOutObject) == typeof(DAL.App.DTO.DomainLikeDTO.ProductSold))
            {
                return MapFromDomain((Domain.ProductSold) inObject) as TOutObject;
            }

            if (typeof(TOutObject) == typeof(Domain.ProductSold))
            {
                return MapFromDAL((DAL.App.DTO.DomainLikeDTO.ProductSold) inObject) as TOutObject;
            }

            throw new InvalidCastException(
                $"No conversion from {inObject.GetType().FullName} to {typeof(TOutObject).FullName}");
        }

        public static DAL.App.DTO.DomainLikeDTO.ProductSold MapFromDomain(Domain.ProductSold productSold)
        {
            var res = productSold == null ? null : new DAL.App.DTO.DomainLikeDTO.ProductSold
                {
                    Id = productSold.Id,
                    ProductId = productSold.ProductId,
                    Product = ProductMapper.MapFromDomain(productSold.Product),
                    ProductSoldTime = productSold.ProductSoldTime,
                    Quantity = productSold.Quantity,
                    SaleId = productSold.SaleId,
                    //Sale = SaleMapper.MapFromDomain(productSold.Sale)
                    SaleAmount = productSold.Quantity * productSold.Product.SellPrice
                };
            
            return res;
        }

        public static Domain.ProductSold MapFromDAL(DAL.App.DTO.DomainLikeDTO.ProductSold productSold)
        {
            var res = productSold == null ? null : new Domain.ProductSold
            {
                Id = productSold.Id,
                ProductId = productSold.ProductId,
                Product = ProductMapper.MapFromDAL(productSold.Product),
                ProductSoldTime = productSold.ProductSoldTime,
                Quantity = productSold.Quantity,
                SaleId = productSold.SaleId,
                Sale = SaleMapper.MapFromDAL(productSold.Sale)
            };
            return res;
        }
    }
}