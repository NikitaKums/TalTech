using System;
using externalDTO = PublicApi.v1.DTO;
using internalDTO = BLL.App.DTO;


namespace PublicApi.v1.Mappers
{
    public class ProductReturnedMapper
    {
        public TOutObject Map<TOutObject>(object inObject)
            where TOutObject : class
        {
            if (typeof(TOutObject) == typeof(externalDTO.ProductReturned))
            {
                // map internal to external
                return MapFromBLL((internalDTO.ProductReturned) inObject) as TOutObject;
            }

            if (typeof(TOutObject) == typeof(internalDTO.ProductReturned))
            {
                // map external to internal
                return MapFromExternal((externalDTO.ProductReturned) inObject) as TOutObject;
            }

            throw new InvalidCastException($"No conversion from {inObject.GetType().FullName} to {typeof(TOutObject).FullName}");
        }


        public static externalDTO.ProductReturned MapFromBLL(internalDTO.ProductReturned productReturned)
        {
            var res = productReturned == null ? null : new externalDTO.ProductReturned()
            {
                Id = productReturned.Id,
                ProductId = productReturned.ProductId,
                ProductName = productReturned.ProductName,
                ProductReturnedTime = productReturned.ProductReturnedTime,
                Quantity = productReturned.Quantity,
                ReturnDescription = productReturned.ReturnDescription,
                ReturnId = productReturned.ReturnId
            };
            return res;
        }
        
        public static externalDTO.ProductReturned MapFromBLL(internalDTO.DomainLikeDTO.ProductReturned productReturned)
        {
            var res = productReturned == null ? null : new externalDTO.ProductReturned()
            {
                Id = productReturned.Id,
                ProductId = productReturned.ProductId,
                ProductReturnedTime = productReturned.ProductReturnedTime,
                Quantity = productReturned.Quantity,
                ReturnId = productReturned.ReturnId
            };
            return res;
        }

        
        public static internalDTO.DomainLikeDTO.ProductReturned MapFromExternal(externalDTO.ProductReturned productReturned)
        {
            var res = productReturned == null ? null : new internalDTO.DomainLikeDTO.ProductReturned()
            {
                Id = productReturned.Id,
                ProductId = productReturned.ProductId,
                ProductReturnedTime = productReturned.ProductReturnedTime,
                Quantity = productReturned.Quantity,
                ReturnId = productReturned.ReturnId
            };
            return res;
        }

    }
}