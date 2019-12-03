using System;
using externalDTO = PublicApi.v1.DTO;
using internalDTO = BLL.App.DTO;

namespace PublicApi.v1.Mappers
{
    public class ProductWithDefectMapper
    {
        public TOutObject Map<TOutObject>(object inObject)
            where TOutObject : class
        {
            if (typeof(TOutObject) == typeof(externalDTO.ProductWithDefect))
            {
                // map internal to external
                return MapFromBLL((internalDTO.ProductWithDefect) inObject) as TOutObject;
            }

            if (typeof(TOutObject) == typeof(internalDTO.ProductWithDefect))
            {
                // map external to internal
                return MapFromExternal((externalDTO.ProductWithDefect) inObject) as TOutObject;
            }

            throw new InvalidCastException($"No conversion from {inObject.GetType().FullName} to {typeof(TOutObject).FullName}");
        }


        public static externalDTO.ProductWithDefect MapFromBLL(internalDTO.ProductWithDefect productWithDefect)
        {
            var res = productWithDefect == null ? null : new externalDTO.ProductWithDefect()
            {
                Id = productWithDefect.Id,
                DefectDescription = productWithDefect.DefectDescription,
                DefectId = productWithDefect.DefectId,
                DefectRecordingTime = productWithDefect.DefectRecordingTime,
                ProductId = productWithDefect.ProductId,
                ProductName = productWithDefect.ProductName,
                Quantity = productWithDefect.Quantity
            };
            return res;
        }
        
        public static externalDTO.ProductWithDefect MapFromBLL(internalDTO.DomainLikeDTO.ProductWithDefect productWithDefect)
        {
            var res = productWithDefect == null ? null : new externalDTO.ProductWithDefect()
            {
                Id = productWithDefect.Id,
                DefectId = productWithDefect.DefectId,
                DefectRecordingTime = productWithDefect.DefectRecordingTime,
                ProductId = productWithDefect.ProductId,
                Quantity = productWithDefect.Quantity
            };
            return res;
        }
        
        public static internalDTO.DomainLikeDTO.ProductWithDefect MapFromExternal(externalDTO.ProductWithDefect productWithDefect)
        {
            var res = productWithDefect == null ? null : new internalDTO.DomainLikeDTO.ProductWithDefect()
            {
                Id = productWithDefect.Id,
                DefectId = productWithDefect.DefectId,
                DefectRecordingTime = productWithDefect.DefectRecordingTime,
                ProductId = productWithDefect.ProductId,
                Quantity = productWithDefect.Quantity
            };
            return res;
        }

    }
}