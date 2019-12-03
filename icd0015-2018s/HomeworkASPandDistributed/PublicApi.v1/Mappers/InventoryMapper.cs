using System;
using externalDTO = PublicApi.v1.DTO;
using internalDTO = BLL.App.DTO;


namespace PublicApi.v1.Mappers
{
    public class InventoryMapper
    {
        public TOutObject Map<TOutObject>(object inObject)
            where TOutObject : class
        {
            if (typeof(TOutObject) == typeof(externalDTO.InventoryWithProductCount))
            {
                // map internal to external
                return MapFromBLL((internalDTO.InventoryWithProductCount) inObject) as TOutObject;
            }

            if (typeof(TOutObject) == typeof(internalDTO.DomainLikeDTO.Inventory))
            {
                // map external to internal
                return MapFromExternal((externalDTO.InventoryWithProductCount) inObject) as TOutObject;
            }

            throw new InvalidCastException($"No conversion from {inObject.GetType().FullName} to {typeof(TOutObject).FullName}");
        }

        public static externalDTO.InventoryWithProductCount MapFromBLL(internalDTO.InventoryWithProductCount inventory)
        {
            var res = inventory == null ? null : new externalDTO.InventoryWithProductCount()
            {
                Id = inventory.Id,
                Description = inventory.Description,
                InventoryCreationTime = inventory.InventoryCreationTime,
                ProductCount = inventory.ProductCount,
                ShopId = inventory.ShopId,
                ShopName = inventory.ShopName
            };
            return res;
        }
        
        public static externalDTO.Inventory MapFromBLL(internalDTO.DomainLikeDTO.Inventory inventory)
        {
            var res = inventory == null ? null : new externalDTO.Inventory()
            {
                Id = inventory.Id,
                Description = inventory.Description,
                InventoryCreationTime = inventory.InventoryCreationTime,
                ShopId = inventory.ShopId,
            };
            return res;
        }
        
        public static internalDTO.DomainLikeDTO.Inventory MapFromExternal(externalDTO.InventoryWithProductCount inventory)
        {
            var res = inventory == null ? null : new internalDTO.DomainLikeDTO.Inventory()
            {
                Id = inventory.Id,
                Description = inventory.Description,
                InventoryCreationTime = inventory.InventoryCreationTime,
                ShopId = inventory.ShopId,
            };
            return res;
        }
        
        public static internalDTO.DomainLikeDTO.Inventory MapFromExternal(externalDTO.Inventory inventory)
        {
            var res = inventory == null ? null : new internalDTO.DomainLikeDTO.Inventory()
            {
                Id = inventory.Id,
                Description = inventory.Description,
                InventoryCreationTime = inventory.InventoryCreationTime,
                ShopId = inventory.ShopId
            };
            return res;
        }

    }
}