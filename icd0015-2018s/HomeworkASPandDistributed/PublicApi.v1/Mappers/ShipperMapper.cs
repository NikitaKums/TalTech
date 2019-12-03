using System;
using BLL.App.DTO.DomainLikeDTO;
using externalDTO = PublicApi.v1.DTO;
using internalDTO = BLL.App.DTO;

namespace PublicApi.v1.Mappers
{
    public class ShipperMapper
    {
        public TOutObject Map<TOutObject>(object inObject)
            where TOutObject : class
        {
            if (typeof(TOutObject) == typeof(externalDTO.Shipper))
            {
                // map internal to external
                return MapFromBLL((internalDTO.ShipperWithOrderCount) inObject) as TOutObject;
            }

            if (typeof(TOutObject) == typeof(internalDTO.ShipperWithOrderCount))
            {
                // map external to internal
                return MapFromExternal((externalDTO.Shipper) inObject) as TOutObject;
            }

            throw new InvalidCastException($"No conversion from {inObject.GetType().FullName} to {typeof(TOutObject).FullName}");
        }


        public static externalDTO.ShipperWithOrderCount MapFromBLL(internalDTO.ShipperWithOrderCount shipper)
        {
            var res = shipper == null ? null : new externalDTO.ShipperWithOrderCount()
            {
                Id = shipper.Id,
                OrdersCount = shipper.OrdersCount,
                PhoneNumber = shipper.PhoneNumber,
                ShipperAddress = shipper.ShipperAddress,
                ShipperName = shipper.ShipperName
            };
            return res;
        }
        
        public static externalDTO.ShipperWithOrderCount MapFromBLL(internalDTO.DomainLikeDTO.Shipper shipper)
        {
            var res = shipper == null ? null : new externalDTO.ShipperWithOrderCount()
            {
                Id = shipper.Id,
                PhoneNumber = shipper.PhoneNumber,
                ShipperAddress = shipper.ShipperAddress,
                ShipperName = shipper.ShipperName
            };
            return res;
        }
        
        public static internalDTO.DomainLikeDTO.Shipper MapFromExternal(externalDTO.Shipper shipper)
        {
            var res = shipper == null ? null : new Shipper()
            {
                Id = shipper.Id,
                PhoneNumber = shipper.PhoneNumber,
                ShipperAddress = shipper.ShipperAddress,
                ShipperName = shipper.ShipperName
            };
            return res;
        }

    }
}