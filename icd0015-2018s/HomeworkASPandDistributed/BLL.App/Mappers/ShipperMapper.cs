using System;
using ee.itcollege.nikita.Contracts.BLL.Base.Mappers;
using internalDTO = DAL.App.DTO.DomainLikeDTO;
using externalDTO = BLL.App.DTO.DomainLikeDTO;

namespace BLL.App.Mappers
{
    public class ShipperMapper : IBaseBLLMapper
    {
        public TOutObject Map<TOutObject>(object inObject) where TOutObject : class
        {
            if (typeof(TOutObject) == typeof(externalDTO.Shipper))
            {
                return MapFromDAL((internalDTO.Shipper) inObject) as TOutObject;
            }

            if (typeof(TOutObject) == typeof(internalDTO.Shipper))
            {
                return MapFromBLL((externalDTO.Shipper) inObject) as TOutObject;
            }

            throw new InvalidCastException($"No conversion from {inObject.GetType().FullName} to {typeof(TOutObject).FullName}");
        }
        
        public static externalDTO.Shipper MapFromDAL(internalDTO.Shipper shipper)
        {
            var res = shipper == null ? null : new externalDTO.Shipper
            {
                Id = shipper.Id,
                ShipperName = shipper.ShipperName,
                PhoneNumber = shipper.PhoneNumber,
                ShipperAddress = shipper.ShipperAddress
            };

            return res;
        }

        public static internalDTO.Shipper MapFromBLL(externalDTO.Shipper shipper)
        {
            var res = shipper == null ? null : new internalDTO.Shipper
            {
                Id = shipper.Id,
                ShipperName = shipper.ShipperName,
                PhoneNumber = shipper.PhoneNumber,
                ShipperAddress = shipper.ShipperAddress
            };

            return res;
        }
        
        public static BLL.App.DTO.ShipperWithOrderCount MapFromDAL(DAL.App.DTO.ShipperWithOrderCount shipper)
        {
            var res = shipper == null ? null : new BLL.App.DTO.ShipperWithOrderCount
            {
                Id = shipper.Id,
                ShipperName = shipper.ShipperName,
                PhoneNumber = shipper.PhoneNumber,
                ShipperAddress = shipper.ShipperAddress,
                OrdersCount = shipper.OrdersCount
            };

            return res;
        }
    }
}