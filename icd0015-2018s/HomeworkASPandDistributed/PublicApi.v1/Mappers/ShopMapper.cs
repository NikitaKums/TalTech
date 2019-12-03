using System;
using externalDTO = PublicApi.v1.DTO;
using internalDTO = BLL.App.DTO;


namespace PublicApi.v1.Mappers
{
    public class ShopMapper
    {
        public TOutObject Map<TOutObject>(object inObject)
            where TOutObject : class
        {
            if (typeof(TOutObject) == typeof(externalDTO.Shop))
            {
                // map internal to external
                return MapFromBLL((internalDTO.ShopWithCounts) inObject) as TOutObject;
            }

            if (typeof(TOutObject) == typeof(internalDTO.ShopWithCounts))
            {
                // map external to internal
                return MapFromExternal((externalDTO.Shop) inObject) as TOutObject;
            }

            throw new InvalidCastException($"No conversion from {inObject.GetType().FullName} to {typeof(TOutObject).FullName}");
        }


        public static externalDTO.Shop MapFromBLL(internalDTO.ShopWithCounts contactType)
        {
            var res = contactType == null ? null : new externalDTO.Shop()
            {
                Id = contactType.Id,
                OrdersCount = contactType.OrdersCount,
                ReturnsCount = contactType.ReturnsCount,
                DefectsCount = contactType.DefectsCount,
                ProductsCount = contactType.ProductsCount,
                AppUsersCount = contactType.AppUsersCount,
                InventoryId = contactType.InventoryId,
                ShopName = contactType.ShopName,
                ShopAddress = contactType.ShopAddress,
                ShopContact = contactType.ShopContact,
                ShopContact2 = contactType.ShopContact2
            };
            return res;
        }
        
        public static externalDTO.Shop MapFromBLL(internalDTO.DomainLikeDTO.Shop contactType)
        {
            var res = contactType == null ? null : new externalDTO.Shop()
            {
                Id = contactType.Id,
                ShopName = contactType.ShopName,
                ShopAddress = contactType.ShopAddress,
                ShopContact = contactType.ShopContact,
                ShopContact2 = contactType.ShopContact2
            };
            return res;
        }
        
        public static internalDTO.DomainLikeDTO.Shop MapFromExternal(externalDTO.Shop contactType)
        {
            var res = contactType == null ? null : new internalDTO.DomainLikeDTO.Shop()
            {
                Id = contactType.Id,
               /* OrdersCount = contactType.OrdersCount,
                ReturnsCount = contactType.ReturnsCount,
                DefectsCount = contactType.DefectsCount,
                ProductsCount = contactType.ProductsCount,
                AppUsersCount = contactType.AppUsersCount,
                InventoryId = contactType.InventoryId,*/
                ShopName = contactType.ShopName,
                ShopAddress = contactType.ShopAddress,
                ShopContact = contactType.ShopContact,
                ShopContact2 = contactType.ShopContact2
            };
            return res;
        }

        
    }
}