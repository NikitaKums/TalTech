using System;
using System.Linq;
using Domain;
using ee.itcollege.nikita.Contracts.DAL.Base.Mappers;

namespace DAL.App.EF.Mappers
{
    public class CategoryMapper : IBaseDALMapper
    {
        public TOutObject Map<TOutObject>(object inObject)
            where TOutObject : class
        {
            if (typeof(TOutObject) == typeof(DAL.App.DTO.DomainLikeDTO.Category))
            {
                return MapFromDomain((Domain.Category) inObject) as TOutObject;
            }

            if (typeof(TOutObject) == typeof(Domain.Category))
            {
                return MapFromDAL((DAL.App.DTO.DomainLikeDTO.Category) inObject) as TOutObject;
            }

            throw new InvalidCastException($"No conversion from {inObject.GetType().FullName} to {typeof(TOutObject).FullName}");
        }

        public static DAL.App.DTO.DomainLikeDTO.Category MapFromDomain(Domain.Category category)
        {
            var res = category == null ? null : new DAL.App.DTO.DomainLikeDTO.Category
                {
                    Id = category.Id,
                    CategoryName = category.CategoryName.Translate(),
                    ShopId = category.ShopId,
                    Shop = ShopMapper.MapFromDomain(category.Shop)
                };
            
            if (category?.ProductsInCategory != null)
            {
                res.ProductsInCategory = category.ProductsInCategory.Select(e => ProductInCategoryMapper.MapFromDomain(e)).ToList();
            }
            
            return res;
        }

        public static Domain.Category MapFromDAL(DAL.App.DTO.DomainLikeDTO.Category category)
        {
            var res = category == null ? null : new Domain.Category
            {
                Id = category.Id,
                CategoryName = new MultiLangString(category.CategoryName),
                ShopId = category.ShopId,
                Shop = ShopMapper.MapFromDAL(category.Shop)
            };
            
            return res;
        }
    }
}