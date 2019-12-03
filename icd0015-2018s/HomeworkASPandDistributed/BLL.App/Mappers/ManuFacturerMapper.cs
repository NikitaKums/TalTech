using System;
using ee.itcollege.nikita.Contracts.BLL.Base.Mappers;
using internalDTO = DAL.App.DTO.DomainLikeDTO;
using externalDTO = BLL.App.DTO.DomainLikeDTO;

namespace BLL.App.Mappers
{
    public class ManuFacturerMapper : IBaseBLLMapper
    {
        public TOutObject Map<TOutObject>(object inObject) where TOutObject : class
        {
            if (typeof(TOutObject) == typeof(externalDTO.ManuFacturer))
            {
                return MapFromDAL((internalDTO.ManuFacturer) inObject) as TOutObject;
            }

            if (typeof(TOutObject) == typeof(internalDTO.ManuFacturer))
            {
                return MapFromBLL((externalDTO.ManuFacturer) inObject) as TOutObject;
            }

            throw new InvalidCastException(
                $"No conversion from {inObject.GetType().FullName} to {typeof(TOutObject).FullName}");
        }

        public static externalDTO.ManuFacturer MapFromDAL(internalDTO.ManuFacturer manuFacturer)
        {
            var res = manuFacturer == null ? null : new  externalDTO.ManuFacturer
            {
                Id = manuFacturer.Id,
                Aadress = manuFacturer.Aadress,
                ManuFacturerName = manuFacturer.ManuFacturerName,
                PhoneNumber = manuFacturer.PhoneNumber
            };

            return res;
        }

        public static internalDTO.ManuFacturer MapFromBLL(externalDTO.ManuFacturer manuFacturer)
        {
            var res = manuFacturer == null ? null : new internalDTO.ManuFacturer
            {
                Id = manuFacturer.Id,
                Aadress = manuFacturer.Aadress,
                ManuFacturerName = manuFacturer.ManuFacturerName,
                PhoneNumber = manuFacturer.PhoneNumber
            };

            return res;
        }

        public static BLL.App.DTO.ManuFacturerWithProductCount MapFromDAL(DAL.App.DTO.ManuFacturerWithProductCount manuFacturer)
        {
            var res = manuFacturer == null ? null : new BLL.App.DTO.ManuFacturerWithProductCount
                {
                    Id = manuFacturer.Id,
                    Aadress = manuFacturer.Aadress,
                    ManuFacturerName = manuFacturer.ManuFacturerName,
                    PhoneNumber = manuFacturer.PhoneNumber,
                    ProductCount = manuFacturer.ProductCount
                };
            return res;
        }
    }
}
