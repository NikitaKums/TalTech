using System;
using ee.itcollege.nikita.Contracts.BLL.Base.Mappers;
using internalDTO = DAL.App.DTO.DomainLikeDTO;
using externalDTO = BLL.App.DTO.DomainLikeDTO;

namespace BLL.App.Mappers
{
    public class ProductReturnedMapper : IBaseBLLMapper
    {
        public TOutObject Map<TOutObject>(object inObject) where TOutObject : class
        {
            if (typeof(TOutObject) == typeof(externalDTO.ProductReturned))
            {
                return MapFromDAL((internalDTO.ProductReturned) inObject) as TOutObject;
            }

            if (typeof(TOutObject) == typeof(internalDTO.ProductReturned))
            {
                return MapFromBLL((externalDTO.ProductReturned) inObject) as TOutObject;
            }

            throw new InvalidCastException($"No conversion from {inObject.GetType().FullName} to {typeof(TOutObject).FullName}");
        }
        
        public static externalDTO.ProductReturned MapFromDAL(internalDTO.ProductReturned productReturned)
        {
            var res = productReturned == null ? null : new externalDTO.ProductReturned
            {
                Id = productReturned.Id,
                ProductId = productReturned.ProductId,
                Product = ProductMapper.MapFromDAL(productReturned.Product),
                ProductReturnedTime = productReturned.ProductReturnedTime,
                Quantity = productReturned.Quantity,
                ReturnId = productReturned.ReturnId,
            };

            if (productReturned?.Return != null)
            {
                res.Return = new BLL.App.DTO.DomainLikeDTO.Return()
                {
                    Description = productReturned.Return.Description
                };
            }

            return res;
        }

        public static internalDTO.ProductReturned MapFromBLL(externalDTO.ProductReturned productReturned)
        {
            var res = productReturned == null ? null : new internalDTO.ProductReturned
            {
                Id = productReturned.Id,
                ProductId = productReturned.ProductId,
                Product = ProductMapper.MapFromBLL(productReturned.Product),
                ProductReturnedTime = productReturned.ProductReturnedTime,
                Quantity = productReturned.Quantity,
                ReturnId = productReturned.ReturnId
            };

            return res;
        }
        
        public static BLL.App.DTO.ProductReturned MapFromDAL(DAL.App.DTO.ProductReturned productReturned)
        {
            var res = productReturned == null ? null : new BLL.App.DTO.ProductReturned
            {
                Id = productReturned.Id,
                ProductId = productReturned.ProductId,
                ProductName = productReturned.ProductName,
                ProductReturnedTime = productReturned.ProductReturnedTime,
                Quantity = productReturned.Quantity,
                ReturnId = productReturned.ReturnId,
                ReturnDescription = productReturned.ReturnDescription
            };
            return res;
        }
    }
}