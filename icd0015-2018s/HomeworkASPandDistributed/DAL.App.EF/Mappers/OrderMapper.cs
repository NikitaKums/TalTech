using System;
using System.Linq;
using DAL.App.DTO.DomainLikeDTO;
using Domain;
using ee.itcollege.nikita.Contracts.DAL.Base.Mappers;

namespace DAL.App.EF.Mappers
{
    public class OrderMapper : IBaseDALMapper
    {
        public TOutObject Map<TOutObject>(object inObject) where TOutObject : class
        {
            if (typeof(TOutObject) == typeof(DAL.App.DTO.DomainLikeDTO.Order))
            {
                return MapFromDomain((Domain.Order) inObject) as TOutObject;
            }

            if (typeof(TOutObject) == typeof(Domain.Order))
            {
                return MapFromDAL((DAL.App.DTO.DomainLikeDTO.Order) inObject) as TOutObject;
            }

            throw new InvalidCastException($"No conversion from {inObject.GetType().FullName} to {typeof(TOutObject).FullName}");
        
        }
        
        public static DAL.App.DTO.DomainLikeDTO.Order MapFromDomain(Domain.Order order)
        {
            var res = order == null ? null : new DAL.App.DTO.DomainLikeDTO.Order
                {
                    Id = order.Id,
                    Description = order.Description.Translate(),
                    OrderCreationTime = order.OrderCreationTime,
                    ShipperId = order.ShipperId,
                    Shipper = ShipperMapper.MapFromDomain(order.Shipper),
                    ShopId = order.ShopId,
                    Shop = ShopMapper.MapFromDomain(order.Shop)
                };

            if (order?.ProductsInOrder != null)
            {
                res.ProductsInOrder = order.ProductsInOrder.Select(e => ProductInOrderMapper.MapFromDomain(e)).ToList();
            }

            return res;
        }

        public static Domain.Order MapFromDAL(DAL.App.DTO.DomainLikeDTO.Order order)
        {
            var res = order == null ? null : new Domain.Order
            {
                Id = order.Id,
                Description = new MultiLangString(order.Description),
                OrderCreationTime = order.OrderCreationTime,
                ShipperId = order.ShipperId,
                Shipper = ShipperMapper.MapFromDAL(order.Shipper),
                ShopId = order.ShopId,
                Shop = ShopMapper.MapFromDAL(order.Shop)
            };

            return res;
        }
    }
}