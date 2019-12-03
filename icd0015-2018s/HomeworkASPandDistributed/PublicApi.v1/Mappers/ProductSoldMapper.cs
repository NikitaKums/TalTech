using System;
using externalDTO = PublicApi.v1.DTO;
using internalDTO = BLL.App.DTO;

namespace PublicApi.v1.Mappers
{
    public class ProductSoldMapper
    {
        public TOutObject Map<TOutObject>(object inObject)
            where TOutObject : class
        {
            if (typeof(TOutObject) == typeof(externalDTO.ProductSold))
            {
                // map internal to external
                return MapFromBLL((internalDTO.ProductSold) inObject) as TOutObject;
            }

            if (typeof(TOutObject) == typeof(internalDTO.ProductSold))
            {
                // map external to internal
                return MapFromExternal((externalDTO.ProductSold) inObject) as TOutObject;
            }

            throw new InvalidCastException(
                $"No conversion from {inObject.GetType().FullName} to {typeof(TOutObject).FullName}");
        }


        public static externalDTO.ProductSold MapFromBLL(internalDTO.ProductSold productSold)
        {
            var res = productSold == null
                ? null
                : new externalDTO.ProductSold()
                {
                    Id = productSold.Id,
                    ProductId = productSold.ProductId,
                    ProductName = productSold.ProductName,
                    ProductSoldTime = productSold.ProductSoldTime,
                    Quantity = productSold.Quantity,
                    SaleDescription = productSold.SaleDescription,
                    SaleId = productSold.SaleId
                };
            return res;
        }
        
        public static externalDTO.ProductSold MapFromBLL(internalDTO.DomainLikeDTO.ProductSold productSold)
        {
            var res = productSold == null
                ? null
                : new externalDTO.ProductSold()
                {
                    Id = productSold.Id,
                    ProductId = productSold.ProductId,
                    ProductSoldTime = productSold.ProductSoldTime,
                    Quantity = productSold.Quantity,
                    SaleId = productSold.SaleId
                };
            return res;
        }

        public static internalDTO.DomainLikeDTO.ProductSold MapFromExternal(externalDTO.ProductSold productSold)
        {
            var res = productSold == null
                ? null
                : new internalDTO.DomainLikeDTO.ProductSold()
                {
                    Id = productSold.Id,
                    ProductId = productSold.ProductId,
                    ProductSoldTime = productSold.ProductSoldTime,
                    Quantity = productSold.Quantity,
                    SaleId = productSold.SaleId
                };
            return res;
        }

        public static externalDTO.IdAndNameDTO.ProductSoldProductSaleIdName MapFromBLL(internalDTO.IdAndNameDTO.ProductSoldProductSaleIdName productSold)
        {
            var res = productSold == null
                ? null
                : new externalDTO.IdAndNameDTO.ProductSoldProductSaleIdName()
                {
                    Id = productSold.Id,
                    ProductId = productSold.ProductId,
                    ProductName = productSold.ProductName,
                    SaleDescription = productSold.SaleDescription,
                    SaleId = productSold.SaleId
                };
            return res;
        }

        public static internalDTO.IdAndNameDTO.ProductSoldProductSaleIdName MapFromExternal(externalDTO.IdAndNameDTO.ProductSoldProductSaleIdName productSold)
        {
            var res = productSold == null
                ? null
                : new internalDTO.IdAndNameDTO.ProductSoldProductSaleIdName()
                {
                    Id = productSold.Id,
                    ProductId = productSold.ProductId,
                    ProductName = productSold.ProductName,
                    SaleDescription = productSold.SaleDescription,
                    SaleId = productSold.SaleId
                };
            return res;
        }
    }
}