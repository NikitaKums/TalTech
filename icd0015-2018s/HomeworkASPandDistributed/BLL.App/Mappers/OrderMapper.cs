using System;
using System.Linq;
using ee.itcollege.nikita.Contracts.BLL.Base.Mappers;
using internalDTO = DAL.App.DTO.DomainLikeDTO;
using externalDTO = BLL.App.DTO.DomainLikeDTO;

namespace BLL.App.Mappers
{
    public class OrderMapper : IBaseBLLMapper
    {
        public TOutObject Map<TOutObject>(object inObject) where TOutObject : class
        {
            if (typeof(TOutObject) == typeof(externalDTO.Order))
            {
                return MapFromDAL((internalDTO.Order) inObject) as TOutObject;
            }

            if (typeof(TOutObject) == typeof(internalDTO.Order))
            {
                return MapFromBLL((externalDTO.Order) inObject) as TOutObject;
            }

            throw new InvalidCastException($"No conversion from {inObject.GetType().FullName} to {typeof(TOutObject).FullName}");
        
        }
        
        public static externalDTO.Order MapFromDAL(internalDTO.Order order)
        {
            var res = order == null ? null : new externalDTO.Order
            {
                Id = order.Id,
                Description = order.Description,
                OrderCreationTime = order.OrderCreationTime,
                ShipperId = order.ShipperId,
                Shipper = ShipperMapper.MapFromDAL(order.Shipper),
                ShopId = order.ShopId,
                Shop = ShopMapper.MapFromDAL(order.Shop),
            };

            if (order?.ProductsInOrder != null)
            {
                res.ProductsInOrder = order.ProductsInOrder.Select(e => ProductInOrderMapper.MapFromDAL(e)).ToList();
            }

            return res;
        }

        public static internalDTO.Order MapFromBLL(externalDTO.Order order)
        {
            var res = order == null ? null : new internalDTO.Order
            {
                Id = order.Id,
                Description = order.Description,
                OrderCreationTime = order.OrderCreationTime,
                ShipperId = order.ShipperId,
                Shipper = ShipperMapper.MapFromBLL(order.Shipper),
                ShopId = order.ShopId,
                Shop = ShopMapper.MapFromBLL(order.Shop),
            };

            return res;
        }
        
        public static BLL.App.DTO.OrderWithProductCount MapFromDAL(DAL.App.DTO.OrderWithProductCount order)
        {
            var res = order == null ? null : new BLL.App.DTO.OrderWithProductCount
            {
                Id = order.Id,
                Description = order.Description,
                OrderCreationTime = order.OrderCreationTime,
                ShipperId = order.ShipperId,
                ShopId = order.ShopId,
                ShopName = order.ShopName,
                ShipperName = order.ShipperName,
                ProductsInOrderCount = order.ProductsInOrderCount
            };

            return res;
        }
    }
}