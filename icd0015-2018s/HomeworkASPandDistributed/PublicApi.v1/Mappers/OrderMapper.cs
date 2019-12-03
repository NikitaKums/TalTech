using System;
using externalDTO = PublicApi.v1.DTO;
using internalDTO = BLL.App.DTO;

namespace PublicApi.v1.Mappers
{
    public class OrderMapper
    {
        public TOutObject Map<TOutObject>(object inObject)
            where TOutObject : class
        {
            if (typeof(TOutObject) == typeof(externalDTO.Order))
            {
                // map internal to external
                return MapFromBLL((internalDTO.OrderWithProductCount) inObject) as TOutObject;
            }

            if (typeof(TOutObject) == typeof(internalDTO.OrderWithProductCount))
            {
                // map external to internal
                return MapFromExternal((externalDTO.Order) inObject) as TOutObject;
            }

            throw new InvalidCastException($"No conversion from {inObject.GetType().FullName} to {typeof(TOutObject).FullName}");
        }

        public static externalDTO.OrderWithProductCount MapFromBLL(internalDTO.OrderWithProductCount order)
        {
            var res = order == null ? null : new externalDTO.OrderWithProductCount()
            {
                Id = order.Id,
                Description = order.Description,
                OrderCreationTime = order.OrderCreationTime,
                ProductsInOrderCount = order.ProductsInOrderCount,
                ShipperId = order.ShipperId,
                ShopId = order.ShopId,
                ShopName = order.ShopName,
                ShipperName = order.ShipperName
            };
            return res;
        }
        
        public static PublicApi.v1.DTO.OrderReceived MapFromBLL(BLL.App.DTO.OrderReceived orderReceived)
        {
            var res = orderReceived == null ? null : new PublicApi.v1.DTO.OrderReceived
            {
                OrderId = orderReceived.OrderId,
                ProductId = orderReceived.ProductId,
                ProductName = orderReceived.ProductName,
                ProductInOrderId = orderReceived.ProductInOrderId,
                Quantity = orderReceived.Quantity
            };
            return res;
        }
        
        public static externalDTO.Order MapFromBLL(internalDTO.DomainLikeDTO.Order order)
        {
            var res = order == null ? null : new externalDTO.Order()
            {
                Id = order.Id,
                Description = order.Description,
                OrderCreationTime = order.OrderCreationTime,
                ShipperId = order.ShipperId,
                ShopId = order.ShopId
            };
            return res;
        }
        
        public static internalDTO.DomainLikeDTO.Order MapFromExternal(externalDTO.Order order)
        {
            var res = order == null ? null : new internalDTO.DomainLikeDTO.Order ()
            {
                Id = order.Id,
                Description = order.Description,
                OrderCreationTime = order.OrderCreationTime,
                ShipperId = order.ShipperId,
                ShopId = order.ShopId,
            };
            return res;
        }

    }
}