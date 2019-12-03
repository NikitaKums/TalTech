using System;
using System.Linq;
using Domain;
using ee.itcollege.nikita.Contracts.DAL.Base.Mappers;

namespace DAL.App.EF.Mappers
{
    public class ProductMapper : IBaseDALMapper
    {
        public TOutObject Map<TOutObject>(object inObject) where TOutObject : class
        {
            if (typeof(TOutObject) == typeof(DAL.App.DTO.DomainLikeDTO.Product))
            {
                return MapFromDomain((Domain.Product) inObject) as TOutObject;
            }

            if (typeof(TOutObject) == typeof(Domain.Product))
            {
                return MapFromDAL((DAL.App.DTO.DomainLikeDTO.Product) inObject) as TOutObject;
            }

            throw new InvalidCastException($"No conversion from {inObject.GetType().FullName} to {typeof(TOutObject).FullName}");
        }
        
        public static DAL.App.DTO.DomainLikeDTO.Product MapFromDomain(Domain.Product product)
        {
            var res = product == null ? null : new DAL.App.DTO.DomainLikeDTO.Product
            {
                Id = product.Id,
                ManuFacturerItemCode = product.ManuFacturerItemCode?.Translate(),
                ShopCode = product.ShopCode?.Translate(),
                ProductName = product.ProductName?.Translate(),
                BuyPrice = product.BuyPrice,
                PercentageAddedToBuyPrice = product.PercentageAddedToBuyPrice,
                SellPrice = product.SellPrice,
                Quantity = product.Quantity,
                Weight = product.Weight?.Translate(),
                Length = product.Length?.Translate()
            };

            if (product?.Manufacturer != null)
            {
                res.ManuFacturerId = product.ManuFacturerId;
                res.Manufacturer = ManuFacturerMapper.MapFromDomain(product.Manufacturer);
                res.InventoryId = product.InventoryId;
                res.Inventory = InventoryMapper.MapFromDomain(product.Inventory);
                res.ShopId = product.ShopId;
                res.Shop = ShopMapper.MapFromDomain(product.Shop);
            }
            
            if (product?.Comments != null)
            {
                res.Comments = product.Comments.Select(e => new DAL.App.DTO.DomainLikeDTO.Comment()
                {
                    CommentTitle = e.CommentTitle.Translate(),
                    CommentBody = e.CommentBody.Translate(),
                    Id = e.Id
                }).ToList();
            }

            return res;
        }

        public static Domain.Product MapFromDAL(DAL.App.DTO.DomainLikeDTO.Product product)
        {
            var res = product == null ? null : new Domain.Product
            {
                Id = product.Id,
                ManuFacturerItemCode = new MultiLangString(product.ManuFacturerItemCode),
                ShopCode = new MultiLangString(product.ShopCode),
                ProductName = new MultiLangString(product.ProductName),
                BuyPrice = product.BuyPrice,
                PercentageAddedToBuyPrice = product.PercentageAddedToBuyPrice,
                SellPrice = product.SellPrice,
                Quantity = product.Quantity,
                Weight = new MultiLangString(product.Weight),
                Length = new MultiLangString(product.Length),
                ManuFacturerId = product.ManuFacturerId,
                Manufacturer = ManuFacturerMapper.MapFromDAL(product.Manufacturer),
                InventoryId = product.InventoryId,
                Inventory = InventoryMapper.MapFromDAL(product.Inventory),
                ShopId = product.ShopId,
                Shop = ShopMapper.MapFromDAL(product.Shop)
            };

            return res;
        }
    }
}