using System;
using System.Linq;
using ee.itcollege.nikita.Contracts.BLL.Base.Mappers;
using internalDTO = DAL.App.DTO;
using externalDTO = BLL.App.DTO;

namespace BLL.App.Mappers
{
    public class CategoryMapper : IBaseBLLMapper
    {
        public TOutObject Map<TOutObject>(object inObject)
            where TOutObject : class
        {
            if (typeof(TOutObject) == typeof(BLL.App.DTO.DomainLikeDTO.Category))
            {
                return MapFromDAL((internalDTO.DomainLikeDTO.Category) inObject) as TOutObject;
            }

            if (typeof(TOutObject) == typeof(DAL.App.DTO.DomainLikeDTO.Category))
            {
                return MapFromBLL((externalDTO.DomainLikeDTO.Category) inObject) as TOutObject;
            }

            throw new InvalidCastException($"No conversion from {inObject.GetType().FullName} to {typeof(TOutObject).FullName}");
        }


        public static BLL.App.DTO.DomainLikeDTO.Category MapFromDAL(DAL.App.DTO.DomainLikeDTO.Category category)
        {
            var res = category == null ? null : new BLL.App.DTO.DomainLikeDTO.Category
            {
                Id = category.Id,
                CategoryName = category.CategoryName,
                ShopId = category.ShopId,
                Shop = ShopMapper.MapFromDAL(category.Shop)
            };
            
            if (category?.ProductsInCategory != null)
            {
                res.ProductsInCategory = category.ProductsInCategory.Select(e => ProductInCategoryMapper.MapFromDAL(e)).ToList();
            }
            return res;
        }
        
        public static DAL.App.DTO.DomainLikeDTO.Category MapFromBLL(BLL.App.DTO.DomainLikeDTO.Category category)
        {
            var res = category == null ? null : new DAL.App.DTO.DomainLikeDTO.Category
            {
                Id = category.Id,
                CategoryName = category.CategoryName,
                ShopId = category.ShopId,
                Shop = ShopMapper.MapFromBLL(category.Shop)
            };
            return res;
        }
        
        
        public static BLL.App.DTO.CategoryWithProductCount MapFromDAL(DAL.App.DTO.CategoryWithProductCount categoryWithProductCount)
        {
            var res = categoryWithProductCount == null ? null : new BLL.App.DTO.CategoryWithProductCount
            {
                Id = categoryWithProductCount.Id,
                CategoryName = categoryWithProductCount.CategoryName,
                CategoryProductCount = categoryWithProductCount.CategoryProductCount
            };
            return res;
        }
        
        public static BLL.App.DTO.IdAndNameDTO.CategoryIdName MapFromDAL(DAL.App.DTO.IdAndNameDTO.CategoryIdName category)
        {
            var res = category == null ? null : new BLL.App.DTO.IdAndNameDTO.CategoryIdName
            {
                Id = category.Id,
                CategoryName = category.CategoryName,
            };
            return res;
        }

    }
}