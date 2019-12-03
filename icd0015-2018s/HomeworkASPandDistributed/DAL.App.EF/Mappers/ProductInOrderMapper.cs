using System;
using ee.itcollege.nikita.Contracts.DAL.Base.Mappers;

namespace DAL.App.EF.Mappers
{
    public class ProductInOrderMapper : IBaseDALMapper
    {
        public TOutObject Map<TOutObject>(object inObject) where TOutObject : class
        {
            if (typeof(TOutObject) == typeof(DAL.App.DTO.DomainLikeDTO.ProductInOrder))
            {
                return MapFromDomain((Domain.ProductInOrder) inObject) as TOutObject;
            }

            if (typeof(TOutObject) == typeof(Domain.ProductInOrder))
            {
                return MapFromDAL((DAL.App.DTO.DomainLikeDTO.ProductInOrder) inObject) as TOutObject;
            }

            throw new InvalidCastException($"No conversion from {inObject.GetType().FullName} to {typeof(TOutObject).FullName}");
        
        }
        
        public static DAL.App.DTO.DomainLikeDTO.ProductInOrder MapFromDomain(Domain.ProductInOrder productInOrder)
        {
            var res = productInOrder == null ? null : new DAL.App.DTO.DomainLikeDTO.ProductInOrder
            {
                Id = productInOrder.Id,
                ProductId = productInOrder.ProductId,
                OrderId = productInOrder.OrderId,
                Product = ProductMapper.MapFromDomain(productInOrder.Product),
                ProductInOrderPlacingTime = productInOrder.ProductInOrderPlacingTime,
                Quantity = productInOrder.Quantity
            };
            
            if (productInOrder?.Order != null)
            {
                res.Order = new DAL.App.DTO.DomainLikeDTO.Order()
                {
                    Description = productInOrder.Order.Description.Translate()
                };
            }

            return res;
        }

        public static Domain.ProductInOrder MapFromDAL(DAL.App.DTO.DomainLikeDTO.ProductInOrder productInOrder)
        {
            var res = productInOrder == null ? null : new Domain.ProductInOrder
            {
                Id = productInOrder.Id,
                ProductId = productInOrder.ProductId,
                OrderId = productInOrder.OrderId,
                //Order = OrderMapper.MapFromDAL(productInOrder.Order),
                Product = ProductMapper.MapFromDAL(productInOrder.Product),
                ProductInOrderPlacingTime = productInOrder.ProductInOrderPlacingTime,
                Quantity = productInOrder.Quantity
            };

            return res;
        }
    }
}