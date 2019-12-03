using System;
using ee.itcollege.nikita.Contracts.BLL.Base.Mappers;
using internalDTO = DAL.App.DTO.DomainLikeDTO;
using externalDTO = BLL.App.DTO.DomainLikeDTO;

namespace BLL.App.Mappers
{
    public class InventoryMapper : IBaseBLLMapper
    {
        public TOutObject Map<TOutObject>(object inObject) where TOutObject : class
        {
            if (typeof(TOutObject) == typeof(externalDTO.Inventory))
            {
                return MapFromDAL((internalDTO.Inventory) inObject) as TOutObject;
            }

            if (typeof(TOutObject) == typeof(internalDTO.Inventory))
            {
                return MapFromBLL((externalDTO.Inventory) inObject) as TOutObject;
            }

            throw new InvalidCastException($"No conversion from {inObject.GetType().FullName} to {typeof(TOutObject).FullName}");
        }
        
        public static externalDTO.Inventory MapFromDAL(internalDTO.Inventory inventory)
        {
            var res = inventory == null ? null : new externalDTO.Inventory
            {
                Id = inventory.Id,
                Description = inventory.Description,
                InventoryCreationTime = inventory.InventoryCreationTime,
                ShopId = inventory.ShopId,
                Shop = ShopMapper.MapFromDAL(inventory.Shop)
            };

            return res;
        }

        public static internalDTO.Inventory MapFromBLL(externalDTO.Inventory inventoryWithProductCount)
        {
            var res = inventoryWithProductCount == null ? null : new internalDTO.Inventory
            {
                Id = inventoryWithProductCount.Id,
                Description = inventoryWithProductCount.Description,
                InventoryCreationTime = inventoryWithProductCount.InventoryCreationTime,
                ShopId = inventoryWithProductCount.ShopId,
                Shop = ShopMapper.MapFromBLL(inventoryWithProductCount.Shop)
            };

            return res;
        }

        public static BLL.App.DTO.InventoryWithProductCount MapFromDAL(DAL.App.DTO.InventoryWithProductCount inventory)
        {
            var res = inventory == null ? null : new BLL.App.DTO.InventoryWithProductCount
                {
                    Id = inventory.Id,
                    Description = inventory.Description,
                    InventoryCreationTime = inventory.InventoryCreationTime,
                    ShopId = inventory.ShopId,
                    ShopName = inventory.ShopName,
                    ProductCount = inventory.ProductCount
                };

            return res;
        }
    }
}