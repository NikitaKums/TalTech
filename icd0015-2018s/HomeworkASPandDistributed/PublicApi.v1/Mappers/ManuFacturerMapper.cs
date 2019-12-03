using System;
using externalDTO = PublicApi.v1.DTO;
using internalDTO = BLL.App.DTO;

namespace PublicApi.v1.Mappers
{
    public class ManuFacturerMapper
    {
        public TOutObject Map<TOutObject>(object inObject)
            where TOutObject : class
        {
            if (typeof(TOutObject) == typeof(externalDTO.ManuFacturer))
            {
                // map internal to external
                return MapFromBLL((internalDTO.ManuFacturerWithProductCount) inObject) as TOutObject;
            }

            if (typeof(TOutObject) == typeof(internalDTO.ManuFacturerWithProductCount))
            {
                // map external to internal
                return MapFromExternal((externalDTO.ManuFacturerWithProductCount) inObject) as TOutObject;
            }

            throw new InvalidCastException($"No conversion from {inObject.GetType().FullName} to {typeof(TOutObject).FullName}");
        }
        
        public static externalDTO.ManuFacturerWithProductCount MapFromBLL(internalDTO.ManuFacturerWithProductCount manuFacturer)
        {
            var res = manuFacturer == null ? null : new externalDTO.ManuFacturerWithProductCount()
            {
                Id = manuFacturer.Id,
                Aadress = manuFacturer.Aadress,
                ManuFacturerName = manuFacturer.ManuFacturerName,
                ProductCount = manuFacturer.ProductCount,
                PhoneNumber = manuFacturer.PhoneNumber
            };
            return res;
        }
        
        public static externalDTO.ManuFacturer MapFromBLL(internalDTO.DomainLikeDTO.ManuFacturer manuFacturer)
        {
            var res = manuFacturer == null ? null : new externalDTO.ManuFacturer()
            {
                Id = manuFacturer.Id,
                Aadress = manuFacturer.Aadress,
                ManuFacturerName = manuFacturer.ManuFacturerName,
                PhoneNumber = manuFacturer.PhoneNumber
            };
            return res;
        }
        
        public static internalDTO.DomainLikeDTO.ManuFacturer MapFromExternal(externalDTO.ManuFacturer manuFacturer)
        {
            var res = manuFacturer == null ? null : new internalDTO.DomainLikeDTO.ManuFacturer 
            {
                Id = manuFacturer.Id,
                Aadress = manuFacturer.Aadress,
                ManuFacturerName = manuFacturer.ManuFacturerName,
                PhoneNumber = manuFacturer.PhoneNumber
            };
            return res;
        }
        
        public static internalDTO.ManuFacturerWithProductCount MapFromExternal(externalDTO.ManuFacturerWithProductCount manuFacturer)
        {
            var res = manuFacturer == null ? null : new internalDTO.ManuFacturerWithProductCount()
            {
                Id = manuFacturer.Id,
                Aadress = manuFacturer.Aadress,
                ManuFacturerName = manuFacturer.ManuFacturerName,
                ProductCount = manuFacturer.ProductCount,
                PhoneNumber = manuFacturer.PhoneNumber
            };
            return res;
        }


    }
}
