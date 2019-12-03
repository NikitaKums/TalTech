using System;
using externalDTO = PublicApi.v1.DTO;
using internalDTO = BLL.App.DTO;


namespace PublicApi.v1.Mappers
{
    public class ProductInOrderMapper
    {
        
        public TOutObject Map<TOutObject>(object inObject)
            where TOutObject : class
        {
            if (typeof(TOutObject) == typeof(externalDTO.ProductInOrder))
            {
                // map internal to external
                return MapFromBLL((internalDTO.ProductInOrder) inObject) as TOutObject;
            }

            if (typeof(TOutObject) == typeof(internalDTO.ProductInOrder))
            {
                // map external to internal
                return MapFromExternal((externalDTO.ProductInOrder) inObject) as TOutObject;
            }

            throw new InvalidCastException($"No conversion from {inObject.GetType().FullName} to {typeof(TOutObject).FullName}");
        }


        public static externalDTO.ProductInOrder MapFromBLL(internalDTO.ProductInOrder productInOrder)
        {
            var res = productInOrder == null ? null : new externalDTO.ProductInOrder()
            {
                Id = productInOrder.Id,
                OrderDescription = productInOrder.OrderDescription,
                OrderId = productInOrder.OrderId,
                ProductId = productInOrder.ProductId,
                ProductInOrderPlacingTime = productInOrder.ProductInOrderPlacingTime,
                ProductName = productInOrder.ProductName,
                Quantity = productInOrder.Quantity
            };
            return res;
        }
        
        public static internalDTO.DomainLikeDTO.ProductInOrder MapFromExternal(externalDTO.ProductInOrder productInOrder)
        {
            var res = productInOrder == null ? null : new internalDTO.DomainLikeDTO.ProductInOrder()
            {
                Id = productInOrder.Id,
                OrderId = productInOrder.OrderId,
                ProductId = productInOrder.ProductId,
                ProductInOrderPlacingTime = productInOrder.ProductInOrderPlacingTime,
                Quantity = productInOrder.Quantity
            };
            return res;
        }
        
        public static externalDTO.ProductInOrder MapFromBLL(internalDTO.DomainLikeDTO.ProductInOrder productInOrder)
        {
            var res = productInOrder == null ? null : new externalDTO.ProductInOrder()
            {
                Id = productInOrder.Id,
                OrderId = productInOrder.OrderId,
                ProductId = productInOrder.ProductId,
                ProductInOrderPlacingTime = productInOrder.ProductInOrderPlacingTime,
                Quantity = productInOrder.Quantity
            };
            return res;
        }

    }
}