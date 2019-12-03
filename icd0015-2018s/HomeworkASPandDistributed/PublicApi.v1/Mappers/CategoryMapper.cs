using System;
using externalDTO = PublicApi.v1.DTO;
using internalDTO = BLL.App.DTO;

namespace PublicApi.v1.Mappers
{
    public class CategoryMapper
    {
        public TOutObject Map<TOutObject>(object inObject)
            where TOutObject : class
        {
            if (typeof(TOutObject) == typeof(externalDTO.Category))
            {
                return MapFromBLL((internalDTO.CategoryWithProductCount) inObject) as TOutObject;
            }

            if (typeof(TOutObject) == typeof(internalDTO.CategoryWithProductCount))
            {
                return MapFromExternal((externalDTO.Category) inObject) as TOutObject;
            }

            throw new InvalidCastException($"No conversion from {inObject.GetType().FullName} to {typeof(TOutObject).FullName}");
        }

        public static externalDTO.CategoryWithProductCount MapFromBLL(internalDTO.CategoryWithProductCount category)
        {
            var res = category == null ? null : new externalDTO.CategoryWithProductCount()
            {
                Id = category.Id,
                CategoryName = category.CategoryName,
                CategoryProductCount = category.CategoryProductCount
            };
            return res;
        }
        
        public static externalDTO.Category MapFromBLL(internalDTO.DomainLikeDTO.Category category)
        {
            var res = category == null ? null : new externalDTO.Category()
            {
                Id = category.Id,
                CategoryName = category.CategoryName,
                ShopId = category.ShopId
            };
            return res;
        }


        public static internalDTO.DomainLikeDTO.Category MapFromExternal(externalDTO.Category category)
        {
            var res = category == null ? null : new internalDTO.DomainLikeDTO.Category()
            {
                Id = category.Id,
                CategoryName = category.CategoryName,
                ShopId = category.ShopId
            };
            return res;
        }
        
        public static externalDTO.IdAndNameDTO.CategoryIdName MapFromBLL(internalDTO.IdAndNameDTO.CategoryIdName category)
        {
            var res = category == null ? null : new externalDTO.IdAndNameDTO.CategoryIdName
            {
                Id = category.Id,
                CategoryName = category.CategoryName,
            };
            return res;
        }
        
        public static internalDTO.IdAndNameDTO.CategoryIdName MapFromExternal(externalDTO.IdAndNameDTO.CategoryIdName category)
        {
            var res = category == null ? null : new internalDTO.IdAndNameDTO.CategoryIdName()
            {
                Id = category.Id,
                CategoryName = category.CategoryName,
            };
            return res;
        }
       
    }
}