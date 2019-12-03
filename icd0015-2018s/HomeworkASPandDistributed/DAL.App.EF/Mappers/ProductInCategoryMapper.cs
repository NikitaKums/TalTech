using System;
using ee.itcollege.nikita.Contracts.DAL.Base.Mappers;

namespace DAL.App.EF.Mappers
{
    public class ProductInCategoryMapper : IBaseDALMapper
    {
        public TOutObject Map<TOutObject>(object inObject) where TOutObject : class
        {
            if (typeof(TOutObject) == typeof(DAL.App.DTO.DomainLikeDTO.ProductInCategory))
            {
                return MapFromDomain((Domain.ProductInCategory) inObject) as TOutObject;
            }

            if (typeof(TOutObject) == typeof(Domain.ProductInCategory))
            {
                return MapFromDAL((DAL.App.DTO.DomainLikeDTO.ProductInCategory) inObject) as TOutObject;
            }

            throw new InvalidCastException(
                $"No conversion from {inObject.GetType().FullName} to {typeof(TOutObject).FullName}");
        }

        public static DAL.App.DTO.DomainLikeDTO.ProductInCategory MapFromDomain(
            Domain.ProductInCategory productInCategory)
        {
            var res = productInCategory == null
                ? null
                : new DAL.App.DTO.DomainLikeDTO.ProductInCategory
                {
                    Id = productInCategory.Id,
                    ProductId = productInCategory.ProductId,
                    //Product = ProductMapper.MapFromDomain(productInCategory.Product),
                    CategoryId = productInCategory.CategoryId,
                    //Category = CategoryMapper.MapFromDomain(productInCategory.Category)
                };

            if (productInCategory?.Product != null)
            {
                res.Product = new DAL.App.DTO.DomainLikeDTO.Product()
                {
                    ProductName = productInCategory.Product.ProductName.Translate()
                };
            }

            if (productInCategory?.Category != null)
            {
                res.Category = new DAL.App.DTO.DomainLikeDTO.Category()
                {
                    CategoryName = productInCategory.Category.CategoryName.Translate()
                };
            }

            return res;
        }

        public static Domain.ProductInCategory MapFromDAL(DAL.App.DTO.DomainLikeDTO.ProductInCategory productInCategory)
        {
            var res = productInCategory == null
                ? null
                : new Domain.ProductInCategory
                {
                    Id = productInCategory.Id,
                    ProductId = productInCategory.ProductId,
                    Product = ProductMapper.MapFromDAL(productInCategory.Product),
                    CategoryId = productInCategory.CategoryId,
                    //Category = CategoryMapper.MapFromDAL(productInCategory.Category)
                };

            return res;
        }
    }
}