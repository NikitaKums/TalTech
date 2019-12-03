using System;
using externalDTO = PublicApi.v1.DTO;
using internalDTO = BLL.App.DTO;
    
namespace PublicApi.v1.Mappers
{
    public class ProductInCategoryMapper
    {
        public TOutObject Map<TOutObject>(object inObject)
            where TOutObject : class
        {
            if (typeof(TOutObject) == typeof(externalDTO.ProductInCategory))
            {
                // map internal to external
                return MapFromBLL((internalDTO.ProductInCategory) inObject) as TOutObject;
            }

            if (typeof(TOutObject) == typeof(internalDTO.ProductInCategory))
            {
                // map external to internal
                return MapFromExternal((externalDTO.ProductInCategory) inObject) as TOutObject;
            }

            throw new InvalidCastException($"No conversion from {inObject.GetType().FullName} to {typeof(TOutObject).FullName}");
        }


        public static externalDTO.ProductInCategory MapFromBLL(internalDTO.ProductInCategory productInCategory)
        {
            var res = productInCategory == null ? null : new externalDTO.ProductInCategory()
            {
                Id = productInCategory.Id,
                CategoryId = productInCategory.CategoryId,
                CategoryName = productInCategory.CategoryName,
                ProductId = productInCategory.ProductId,
                ProductName = productInCategory.ProductName
            };
            return res;
        }
        
        public static externalDTO.ProductInCategory MapFromBLL(internalDTO.DomainLikeDTO.ProductInCategory productInCategory)
        {
            var res = productInCategory == null ? null : new externalDTO.ProductInCategory()
            {
                Id = productInCategory.Id,
                CategoryId = productInCategory.CategoryId,
                ProductId = productInCategory.ProductId,
            };
            return res;
        }
        
        public static internalDTO.DomainLikeDTO.ProductInCategory MapFromExternal(externalDTO.ProductInCategory productInCategory)
        {
            var res = productInCategory == null ? null : new internalDTO.DomainLikeDTO.ProductInCategory()
            {
                Id = productInCategory.Id,
                CategoryId = productInCategory.CategoryId,
                ProductId = productInCategory.ProductId,
            };
            return res;
        }


        
    }
}