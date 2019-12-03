using System;
using ee.itcollege.nikita.Contracts.DAL.Base.Mappers;

namespace DAL.App.EF.Mappers
{
    public class ProductReturnedMapper : IBaseDALMapper
    {
        public TOutObject Map<TOutObject>(object inObject) where TOutObject : class
        {
            if (typeof(TOutObject) == typeof(DAL.App.DTO.DomainLikeDTO.ProductReturned))
            {
                return MapFromDomain((Domain.ProductReturned) inObject) as TOutObject;
            }

            if (typeof(TOutObject) == typeof(Domain.ProductReturned))
            {
                return MapFromDAL((DAL.App.DTO.DomainLikeDTO.ProductReturned) inObject) as TOutObject;
            }

            throw new InvalidCastException($"No conversion from {inObject.GetType().FullName} to {typeof(TOutObject).FullName}");
        }
        
        public static DAL.App.DTO.DomainLikeDTO.ProductReturned MapFromDomain(Domain.ProductReturned productReturned)
        {
            var res = productReturned == null ? null : new DAL.App.DTO.DomainLikeDTO.ProductReturned
            {
                Id = productReturned.Id,
                ProductId = productReturned.ProductId,
                Product = ProductMapper.MapFromDomain(productReturned.Product),
                ProductReturnedTime = productReturned.ProductReturnedTime,
                Quantity = productReturned.Quantity,
                ReturnId = productReturned.ReturnId,
            };

            if (productReturned?.Return != null)
            {
                res.Return = new DAL.App.DTO.DomainLikeDTO.Return()
                {
                    Description = productReturned.Return.Description.Translate()
                };
            }

            return res;
        }

        public static Domain.ProductReturned MapFromDAL(DAL.App.DTO.DomainLikeDTO.ProductReturned productReturned)
        {
            var res = productReturned == null ? null : new Domain.ProductReturned
            {
                Id = productReturned.Id,
                ProductId = productReturned.ProductId,
                Product = ProductMapper.MapFromDAL(productReturned.Product),
                ProductReturnedTime = productReturned.ProductReturnedTime,
                Quantity = productReturned.Quantity,
                ReturnId = productReturned.ReturnId,
                //Return = ReturnMapper.MapFromDAL(productReturned.Return)
            };

            return res;
        }
    }
}