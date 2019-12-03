using System;
using ee.itcollege.nikita.Contracts.BLL.Base.Mappers;
using internalDTO = DAL.App.DTO.DomainLikeDTO;
using externalDTO = BLL.App.DTO.DomainLikeDTO;

namespace BLL.App.Mappers
{
    public class ProductInOrderMapper : IBaseBLLMapper
    {
        public TOutObject Map<TOutObject>(object inObject) where TOutObject : class
        {
            if (typeof(TOutObject) == typeof(externalDTO.ProductInOrder))
            {
                return MapFromDAL((internalDTO.ProductInOrder) inObject) as TOutObject;
            }

            if (typeof(TOutObject) == typeof(internalDTO.ProductInOrder))
            {
                return MapFromBLL((externalDTO.ProductInOrder) inObject) as TOutObject;
            }

            throw new InvalidCastException($"No conversion from {inObject.GetType().FullName} to {typeof(TOutObject).FullName}");
        
        }
        
        public static externalDTO.ProductInOrder MapFromDAL(internalDTO.ProductInOrder productInOrder)
        {
            var res = productInOrder == null ? null : new externalDTO.ProductInOrder
            {
                Id = productInOrder.Id,
                ProductId = productInOrder.ProductId,
                OrderId = productInOrder.OrderId,
                Product = ProductMapper.MapFromDAL(productInOrder.Product),
                ProductInOrderPlacingTime = productInOrder.ProductInOrderPlacingTime,
                Quantity = productInOrder.Quantity
            };

            if (productInOrder?.Order != null)
            {
                res.Order = new BLL.App.DTO.DomainLikeDTO.Order()
                {
                    Description = productInOrder.Order.Description
                };
            }

            return res;
        }
        
        public static BLL.App.DTO.OrderReceived MapFromDAL(DAL.App.DTO.OrderReceived orderReceived)
        {
            var res = orderReceived == null ? null : new BLL.App.DTO.OrderReceived 
            {
                OrderId = orderReceived.OrderId,
                ProductId = orderReceived.ProductId,
                Quantity = orderReceived.Quantity,
                ProductInOrderId = orderReceived.ProductInOrderId,
                ProductName = orderReceived.ProductName
            };

            return res;
        }

        public static internalDTO.ProductInOrder MapFromBLL(externalDTO.ProductInOrder productInOrder)
        {
            var res = productInOrder == null ? null : new internalDTO.ProductInOrder
            {
                Id = productInOrder.Id,
                ProductId = productInOrder.ProductId,
                OrderId = productInOrder.OrderId,
                Product = ProductMapper.MapFromBLL(productInOrder.Product),
                ProductInOrderPlacingTime = productInOrder.ProductInOrderPlacingTime,
                Quantity = productInOrder.Quantity
            };

            return res;
        }
        
        public static BLL.App.DTO.ProductInOrder MapFromDAL(DAL.App.DTO.ProductInOrder productInOrder)
        {
            var res = productInOrder == null ? null : new BLL.App.DTO.ProductInOrder
            {
                Id = productInOrder.Id,
                ProductId = productInOrder.ProductId,
                OrderId = productInOrder.OrderId,
                OrderDescription = productInOrder.OrderDescription,
                ProductName = productInOrder.ProductName,
                ProductInOrderPlacingTime = productInOrder.ProductInOrderPlacingTime,
                Quantity = productInOrder.Quantity
            };

            return res;
        }
    }
}