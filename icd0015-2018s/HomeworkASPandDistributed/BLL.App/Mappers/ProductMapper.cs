using System;
using System.Linq;
using ee.itcollege.nikita.Contracts.BLL.Base.Mappers;
using internalDTO = DAL.App.DTO.DomainLikeDTO;
using externalDTO = BLL.App.DTO.DomainLikeDTO;

namespace BLL.App.Mappers
{
    public class ProductMapper : IBaseBLLMapper
    {
         public TOutObject Map<TOutObject>(object inObject) where TOutObject : class
        {
            if (typeof(TOutObject) == typeof(externalDTO.Product))
            {
                return MapFromDAL((internalDTO.Product) inObject) as TOutObject;
            }

            if (typeof(TOutObject) == typeof(internalDTO.Product))
            {
                return MapFromBLL((externalDTO.Product) inObject) as TOutObject;
            }

            throw new InvalidCastException($"No conversion from {inObject.GetType().FullName} to {typeof(TOutObject).FullName}");
        }
        
        public static externalDTO.Product MapFromDAL(internalDTO.Product product)
        {
            var res = product == null ? null : new externalDTO.Product
            {
                Id = product.Id,
                ManuFacturerItemCode = product.ManuFacturerItemCode,
                ShopCode = product.ShopCode,
                ProductName = product.ProductName,
                BuyPrice = product.BuyPrice,
                PercentageAddedToBuyPrice = product.PercentageAddedToBuyPrice,
                SellPrice = product.SellPrice,
                Quantity = product.Quantity,
                Weight = product.Weight,
                Length = product.Length,
            };
            
            if (product?.Manufacturer != null)
            {
                res.ManuFacturerId = product.ManuFacturerId;
                res.Manufacturer = ManuFacturerMapper.MapFromDAL(product.Manufacturer);
                res.InventoryId = product.InventoryId;
                res.Inventory = InventoryMapper.MapFromDAL(product.Inventory);
                res.ShopId = product.ShopId;
                res.Shop = ShopMapper.MapFromDAL(product.Shop);
            }
            
            if (product?.Comments != null)
            {
                res.Comments = product.Comments.Select(e => new BLL.App.DTO.DomainLikeDTO.Comment()
                {
                    CommentTitle = e.CommentTitle,
                    CommentBody = e.CommentBody,
                    Id = e.Id
                }).ToList();
            }

            return res;
        }

        public static internalDTO.Product MapFromBLL(externalDTO.Product product)
        {
            var res = product == null ? null : new internalDTO.Product
            {
                Id = product.Id,
                ManuFacturerItemCode = product.ManuFacturerItemCode,
                ShopCode = product.ShopCode,
                ProductName = product.ProductName,
                BuyPrice = product.BuyPrice,
                PercentageAddedToBuyPrice = product.PercentageAddedToBuyPrice,
                SellPrice = product.SellPrice,
                Quantity = product.Quantity,
                Weight = product.Weight,
                Length = product.Length,
                ManuFacturerId = product.ManuFacturerId,
                Manufacturer = ManuFacturerMapper.MapFromBLL(product.Manufacturer),
                InventoryId = product.InventoryId,
                Inventory = InventoryMapper.MapFromBLL(product.Inventory),
                ShopId = product.ShopId,
                Shop = ShopMapper.MapFromBLL(product.Shop)
            };

            return res;
        }
        
        public static BLL.App.DTO.ProductWithCounts MapFromDAL(DAL.App.DTO.ProductWithCounts product)
        {
            var res = product == null ? null : new BLL.App.DTO.ProductWithCounts
            {
                Id = product.Id,
                ManuFacturerItemCode = product.ManuFacturerItemCode,
                ShopCode = product.ShopCode,
                ProductName = product.ProductName,
                BuyPrice = product.BuyPrice,
                PercentageAddedToBuyPrice = product.PercentageAddedToBuyPrice,
                SellPrice = product.SellPrice,
                Quantity = product.Quantity,
                Weight = product.Weight,
                Length = product.Length,
                ManuFacturerId = product.ManuFacturerId,
                ManuFacturerName = product.ManuFacturerName,
                InventoryId = product.InventoryId,
                InventoryName = product.InventoryName,
                ShopId = product.ShopId,
                ShopName = product.ShopName,
                ProductsWithDefectCount = product.ProductsWithDefectCount,
                ProductReturnsCount = product.ProductReturnsCount,
                ProductsSoldCount = product.ProductsSoldCount,
                ProductsInOrdersCount = product.ProductsInOrdersCount,
            };

            if (product?.CategoryDTOs != null)
            {
                res.CategoryDTOs = product.CategoryDTOs.Select(e => CategoryMapper.MapFromDAL(e)).ToList();
            }
            
            if (product?.CommentDTOs != null)
            {
                res.CommentDTOs = product.CommentDTOs.Select(e => CommentMapper.MapFromDAL(e)).ToList();
            }

            return res;
        }
    }
}