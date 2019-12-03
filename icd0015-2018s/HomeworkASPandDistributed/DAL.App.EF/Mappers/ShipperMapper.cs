using System;
using Domain;
using ee.itcollege.nikita.Contracts.DAL.Base.Mappers;

namespace DAL.App.EF.Mappers
{
    public class ShipperMapper : IBaseDALMapper
    {
        public TOutObject Map<TOutObject>(object inObject) where TOutObject : class
        {
            if (typeof(TOutObject) == typeof(DAL.App.DTO.DomainLikeDTO.Shipper))
            {
                return MapFromDomain((Domain.Shipper) inObject) as TOutObject;
            }

            if (typeof(TOutObject) == typeof(Domain.Shipper))
            {
                return MapFromDAL((DAL.App.DTO.DomainLikeDTO.Shipper) inObject) as TOutObject;
            }

            throw new InvalidCastException($"No conversion from {inObject.GetType().FullName} to {typeof(TOutObject).FullName}");
        }
        
        public static DAL.App.DTO.DomainLikeDTO.Shipper MapFromDomain(Domain.Shipper shipper)
        {
            var res = shipper == null ? null : new DAL.App.DTO.DomainLikeDTO.Shipper
                {
                    Id = shipper.Id,
                    ShipperName = shipper.ShipperName.Translate(),
                    PhoneNumber = shipper.PhoneNumber.Translate(),
                    ShipperAddress = shipper.ShipperAddress.Translate()
                };

            return res;
        }

        public static Domain.Shipper MapFromDAL(DAL.App.DTO.DomainLikeDTO.Shipper shipper)
        {
            var res = shipper == null ? null : new Domain.Shipper
            {
                Id = shipper.Id,
                ShipperName = new MultiLangString(shipper.ShipperName),
                PhoneNumber = new MultiLangString(shipper.PhoneNumber),
                ShipperAddress = new MultiLangString(shipper.ShipperAddress)
            };

            return res;
        }
    }
}