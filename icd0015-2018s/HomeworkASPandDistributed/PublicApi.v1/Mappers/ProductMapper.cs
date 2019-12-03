using System;
using System.Linq;
using externalDTO = PublicApi.v1.DTO;
using internalDTO = BLL.App.DTO;

namespace PublicApi.v1.Mappers
{
    public class ProductMapper
    {
        public TOutObject Map<TOutObject>(object inObject)
            where TOutObject : class
        {
            if (typeof(TOutObject) == typeof(externalDTO.Product))
            {
                // map internal to external
                return MapFromBLL((internalDTO.ProductWithCounts) inObject) as TOutObject;
            }

            if (typeof(TOutObject) == typeof(internalDTO.ProductWithCounts))
            {
                // map external to internal
                return MapFromExternal((externalDTO.Product) inObject) as TOutObject;
            }

            throw new InvalidCastException($"No conversion from {inObject.GetType().FullName} to {typeof(TOutObject).FullName}");
        }


        public static externalDTO.ProductWithDataStuff MapFromBLL(internalDTO.ProductWithCounts product)
        {
            var res = product == null ? null : new externalDTO.ProductWithDataStuff()
            {
                Id = product.Id,
                BuyPrice = product.BuyPrice,
                ManuFacturerItemCode = product.ManuFacturerItemCode,
                ShopCode = product.ShopCode,
                ProductName = product.ProductName,
                PercentageAddedToBuyPrice = product.PercentageAddedToBuyPrice,
                SellPrice = product.SellPrice,
                Quantity = product.Quantity,
                Weight = product.Weight,
                Length = product.Length,
                ManuFacturerId = product.ManuFacturerId,
                ManuFacturerName = product.ManuFacturerName,
                ShopId = product.ShopId,
                ShopName = product.ShopName,
                InventoryId = product.InventoryId,
                InventoryName = product.InventoryName,
                ProductsWithDefectCount = product.ProductsWithDefectCount,
                ProductReturnsCount = product.ProductReturnsCount,
                ProductsSoldCount = product.ProductsSoldCount,
                ProductsInOrdersCount = product.ProductsInOrdersCount,
            };

            if (product?.CategoryDTOs != null)
            {
                res.CategoryDTOs = product.CategoryDTOs.Select(e => CategoryMapper.MapFromBLL(e)).ToList();
            }
            
            if (product?.CommentDTOs != null)
            {
                res.CommentDTOs = product.CommentDTOs.Select(e => CommentMapper.MapFromBLL(e)).ToList();
            }
            
            return res;
        }
        
        public static internalDTO.DomainLikeDTO.Product MapFromExternal(externalDTO.Product product)
        {
            var res = product == null ? null : new internalDTO.DomainLikeDTO.Product()
            {
                Id = product.Id,
                BuyPrice = product.BuyPrice,
                ManuFacturerItemCode = product.ManuFacturerItemCode,
                ShopCode = product.ShopCode,
                ProductName = product.ProductName,
                PercentageAddedToBuyPrice = product.PercentageAddedToBuyPrice,
                SellPrice = product.SellPrice,
                Quantity = product.Quantity,
                Weight = product.Weight,
                Length = product.Length,
                ManuFacturerId = product.ManuFacturerId,
                ShopId = product.ShopId,
                InventoryId = product.InventoryId,
                /*ProductsWithDefectCount = product.ProductsWithDefectCount,
                ProductReturnsCount = product.ProductReturnsCount,
                ProductsSoldCount = product.ProductsSoldCount,
                ProductsInOrdersCount = product.ProductsInOrdersCount,*/
            };
            
            /*if (product?.CategoryDTOs != null)
            {
                res.CategoryDTOs = product.CategoryDTOs.Select(e => CategoryMapper.MapFromExternal(e)).ToList();
            }
            
            if (product?.CommentDTOs != null)
            {
                res.CommentDTOs = product.CommentDTOs.Select(e => CommentMapper.MapFromExternal(e)).ToList();
            }*/
            return res;
        }
        
        public static externalDTO.Product MapFromBLL(internalDTO.DomainLikeDTO.Product product)
        {
            var res = product == null ? null : new externalDTO.Product()
            {
                Id = product.Id,
                BuyPrice = product.BuyPrice,
                ManuFacturerItemCode = product.ManuFacturerItemCode,
                ShopCode = product.ShopCode,
                ProductName = product.ProductName,
                PercentageAddedToBuyPrice = product.PercentageAddedToBuyPrice,
                SellPrice = product.SellPrice,
                Quantity = product.Quantity,
                Weight = product.Weight,
                Length = product.Length,
                ManuFacturerId = product.ManuFacturerId,
                ShopId = product.ShopId,
                InventoryId = product.InventoryId
            };
            return res;
        }
        
        public static externalDTO.IdAndNameDTO.ProductIdName MapFromBLL(internalDTO.IdAndNameDTO.ProductIdName product)
        {
            var res = product == null ? null : new externalDTO.IdAndNameDTO.ProductIdName()
            {
               Id = product.Id,
               ProductName = product.ProductName,
               ProductInCategoryId = product.ProductInCategoryId,
               ProductWithDefectId = product.ProductWithDefectId
            };
            return res;
        }
        
        public static internalDTO.IdAndNameDTO.ProductIdName MapFromExternal(externalDTO.IdAndNameDTO.ProductIdName product)
        {
            var res = product == null ? null : new internalDTO.IdAndNameDTO.ProductIdName()
            {
                Id = product.Id,
                ProductName = product.ProductName
            };
            return res;
        }

    }
}