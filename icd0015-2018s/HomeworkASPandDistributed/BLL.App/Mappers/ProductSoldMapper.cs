using System;
using ee.itcollege.nikita.Contracts.BLL.Base.Mappers;
using internalDTO = DAL.App.DTO.DomainLikeDTO;
using externalDTO = BLL.App.DTO.DomainLikeDTO;

namespace BLL.App.Mappers
{
    public class ProductSoldMapper : IBaseBLLMapper
    {
        public TOutObject Map<TOutObject>(object inObject) where TOutObject : class
        {
            if (typeof(TOutObject) == typeof(externalDTO.ProductSold))
            {
                return MapFromDAL((internalDTO.ProductSold) inObject) as TOutObject;
            }

            if (typeof(TOutObject) == typeof(internalDTO.ProductSold))
            {
                return MapFromBLL((externalDTO.ProductSold) inObject) as TOutObject;
            }

            throw new InvalidCastException(
                $"No conversion from {inObject.GetType().FullName} to {typeof(TOutObject).FullName}");
        }

        public static externalDTO.ProductSold MapFromDAL(internalDTO.ProductSold productSold)
        {
            var res = productSold == null ? null : new externalDTO.ProductSold
            {
                Id = productSold.Id,
                ProductId = productSold.ProductId,
                Product = ProductMapper.MapFromDAL(productSold.Product),
                ProductSoldTime = productSold.ProductSoldTime,
                Quantity = productSold.Quantity,
                SaleId = productSold.SaleId,
                //Sale = SaleMapper.MapFromDAL(productSold.Sale)
                SaleAmount = productSold.Quantity * productSold.Product?.SellPrice
            };
      
            return res;
        }

        public static internalDTO.ProductSold MapFromBLL(externalDTO.ProductSold productSold)
        {
            var res = productSold == null ? null : new internalDTO.ProductSold
            {
                Id = productSold.Id,
                ProductId = productSold.ProductId,
                Product = ProductMapper.MapFromBLL(productSold.Product),
                ProductSoldTime = productSold.ProductSoldTime,
                Quantity = productSold.Quantity,
                SaleId = productSold.SaleId,
                Sale = SaleMapper.MapFromBLL(productSold.Sale)
            };
            return res;
        }
        
        public static BLL.App.DTO.ProductSold MapFromDAL(DAL.App.DTO.ProductSold productSold)
        {
            var res = productSold == null ? null : new BLL.App.DTO.ProductSold
            {
                Id = productSold.Id,
                ProductId = productSold.ProductId,
                ProductName = productSold.ProductName,
                ProductSoldTime = productSold.ProductSoldTime,
                Quantity = productSold.Quantity,
                SaleId = productSold.SaleId,
                SaleDescription = productSold.SaleDescription
            };
            return res;
        }
    }
}