using System;
using System.Linq;
using externalDTO = PublicApi.v1.DTO;
using internalDTO = BLL.App.DTO;


namespace PublicApi.v1.Mappers
{
    public class ReturnMapper
    {
        public TOutObject Map<TOutObject>(object inObject)
            where TOutObject : class
        {
            if (typeof(TOutObject) == typeof(externalDTO.Return))
            {
                // map internal to external
                return MapFromBLL((internalDTO.ReturnWithProductCount) inObject) as TOutObject;
            }

            if (typeof(TOutObject) == typeof(internalDTO.ReturnWithProductCount))
            {
                // map external to internal
                return MapFromExternal((externalDTO.Return) inObject) as TOutObject;
            }

            throw new InvalidCastException($"No conversion from {inObject.GetType().FullName} to {typeof(TOutObject).FullName}");
        }


        public static externalDTO.Return MapFromBLL(internalDTO.ReturnWithProductCount @return)
        {
            var res = @return == null ? null : new externalDTO.Return()
            {
                Id = @return.Id,
                Description = @return.Description,
                ProductsReturnedCount = @return.ProductsReturnedCount,
                ShopId = @return.ShopId,
                ShopName = @return.ShopName
            };

            if (@return?.ProductIdNameDtos != null)
            {
                res.ProductIdNameDtos = @return.ProductIdNameDtos.Select(e => ProductMapper.MapFromBLL(e)).ToList();

            }
            return res;
        }
        
        public static externalDTO.Return MapFromBLL(internalDTO.DomainLikeDTO.Return @return)
        {
            var res = @return == null ? null : new externalDTO.Return()
            {
                Id = @return.Id,
                Description = @return.Description,
                ShopId = @return.ShopId,
            };
            return res;
        }
        
        public static externalDTO.IdAndNameDTO.ReturnIdName MapFromBLL(internalDTO.IdAndNameDTO.ReturnIdName @return)
        {
            var res = @return == null ? null : new externalDTO.IdAndNameDTO.ReturnIdName()
            {
                Id = @return.Id,
                ReturnDescription = @return.ReturnDescription
            };
            return res;
        }
        
        public static internalDTO.DomainLikeDTO.Return MapFromExternal(externalDTO.Return @return)
        {
            var res = @return == null ? null : new internalDTO.DomainLikeDTO.Return()
            {
                Id = @return.Id,
                Description = @return.Description,
                ShopId = @return.ShopId
            };
            return res;
        }
    }
}