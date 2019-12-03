using System;
using externalDTO = PublicApi.v1.DTO;
using internalDTO = BLL.App.DTO;

namespace PublicApi.v1.Mappers
{
    public class DefectMapper
    {
        public TOutObject Map<TOutObject>(object inObject)
            where TOutObject : class
        {
            if (typeof(TOutObject) == typeof(externalDTO.Defect))
            {
                // map internal to external
                return MapFromBLL((internalDTO.DefectWithProductCount) inObject) as TOutObject;
            }

            if (typeof(TOutObject) == typeof(internalDTO.DefectWithProductCount))
            {
                // map external to internal
                return MapFromExternal((externalDTO.Defect) inObject) as TOutObject;
            }

            throw new InvalidCastException($"No conversion from {inObject.GetType().FullName} to {typeof(TOutObject).FullName}");
        }
        
        public static externalDTO.Defect MapFromBLL(internalDTO.DefectWithProductCount defect)
        {
            var res = defect == null ? null : new externalDTO.Defect()
            {
                Id = defect.Id,
                Description = defect.Description,
                ProductsWithDefectCount = defect.ProductsWithDefectCount,
                ShopId = defect.ShopId,
                ShopName = defect.ShopName
            };
            return res;
        }
        
        public static externalDTO.Defect MapFromBLL(internalDTO.DomainLikeDTO.Defect defect)
        {
            var res = defect == null ? null : new externalDTO.Defect()
            {
                Id = defect.Id,
                Description = defect.Description,
                ShopId = defect.ShopId,
            };
            return res;
        }
        
        public static internalDTO.DomainLikeDTO.Defect MapFromExternal(externalDTO.Defect defect)
        {
            var res = defect == null ? null : new internalDTO.DomainLikeDTO.Defect ()
            {
                Id = defect.Id,
                Description = defect.Description,
                ShopId = defect.ShopId,
            };
            return res;
        }

    }
}