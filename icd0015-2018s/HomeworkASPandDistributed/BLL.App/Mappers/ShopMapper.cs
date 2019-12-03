using System;
using ee.itcollege.nikita.Contracts.BLL.Base.Mappers;
using internalDTO = DAL.App.DTO.DomainLikeDTO;
using externalDTO = BLL.App.DTO.DomainLikeDTO;


namespace BLL.App.Mappers
{
    public class ShopMapper : IBaseBLLMapper
    {
        public TOutObject Map<TOutObject>(object inObject)
            where TOutObject : class
        {
            if (typeof(TOutObject) == typeof(externalDTO.Shop))
            {
                return MapFromDAL((internalDTO.Shop) inObject) as TOutObject;
            }

            if (typeof(TOutObject) == typeof(internalDTO.Shop))
            {
                return MapFromBLL((externalDTO.Shop) inObject) as TOutObject;
            }

            throw new InvalidCastException($"No conversion from {inObject.GetType().FullName} to {typeof(TOutObject).FullName}");
        }


        public static externalDTO.Shop MapFromDAL(internalDTO.Shop shop)
        {
            var res = shop == null ? null : new externalDTO.Shop
            {
                Id = shop.Id,
                ShopName = shop.ShopName,
                ShopAddress = shop.ShopAddress,
                ShopContact = shop.ShopContact,
                ShopContact2 = shop.ShopContact2
            };
            return res;
        }
        
        public static internalDTO.Shop MapFromBLL(externalDTO.Shop shop)
        {
            var res = shop == null ? null : new internalDTO.Shop
            {
                Id = shop.Id,
                ShopName = shop.ShopName,
                ShopAddress = shop.ShopAddress,
                ShopContact = shop.ShopContact,
                ShopContact2 = shop.ShopContact2
            };
            
            return res;
        }
        public static BLL.App.DTO.ShopWithCounts MapFromDAL(DAL.App.DTO.ShopWithCounts shop)
        {
            var res = shop == null ? null : new BLL.App.DTO.ShopWithCounts
            {
                Id = shop.Id,
                ShopName = shop.ShopName,
                InventoryId = shop.InventoryId,
                ShopAddress = shop.ShopAddress,
                ShopContact = shop.ShopContact,
                ShopContact2 = shop.ShopContact2,
                OrdersCount = shop.OrdersCount,
                DefectsCount = shop.DefectsCount,
                ReturnsCount = shop.ReturnsCount,
                AppUsersCount = shop.AppUsersCount,
                ProductsCount = shop.ProductsCount,
            };
            return res;
        }
        
    }
}