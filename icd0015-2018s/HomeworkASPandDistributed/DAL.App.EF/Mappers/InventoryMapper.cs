using System;
using DAL.App.DTO.DomainLikeDTO;
using Domain;
using ee.itcollege.nikita.Contracts.DAL.Base.Mappers;

namespace DAL.App.EF.Mappers
{
    public class InventoryMapper : IBaseDALMapper
    {
        public TOutObject Map<TOutObject>(object inObject) where TOutObject : class
        {
            if (typeof(TOutObject) == typeof(DAL.App.DTO.DomainLikeDTO.Inventory))
            {
                return MapFromDomain((Domain.Inventory) inObject) as TOutObject;
            }

            if (typeof(TOutObject) == typeof(Domain.Inventory))
            {
                return MapFromDAL((DAL.App.DTO.DomainLikeDTO.Inventory) inObject) as TOutObject;
            }

            throw new InvalidCastException($"No conversion from {inObject.GetType().FullName} to {typeof(TOutObject).FullName}");
        }
        
        public static DAL.App.DTO.DomainLikeDTO.Inventory MapFromDomain(Domain.Inventory inventory)
        {
            var res = inventory == null ? null : new DAL.App.DTO.DomainLikeDTO.Inventory
                {
                    Id = inventory.Id,
                    Description = inventory.Description?.Translate(),
                    InventoryCreationTime = inventory.InventoryCreationTime,
                    ShopId = inventory.ShopId,
                    Shop = ShopMapper.MapFromDomain(inventory.Shop)
                };

            return res;
        }
        

        public static Domain.Inventory MapFromDAL(DAL.App.DTO.DomainLikeDTO.Inventory inventoryWithProductCount)
        {
            var res = inventoryWithProductCount == null ? null : new Domain.Inventory
            {
                Id = inventoryWithProductCount.Id,
                Description = new MultiLangString(inventoryWithProductCount.Description),
                InventoryCreationTime = inventoryWithProductCount.InventoryCreationTime,
                ShopId = inventoryWithProductCount.ShopId,
                Shop = ShopMapper.MapFromDAL(inventoryWithProductCount.Shop)
            };

            return res;
        }
    }
}