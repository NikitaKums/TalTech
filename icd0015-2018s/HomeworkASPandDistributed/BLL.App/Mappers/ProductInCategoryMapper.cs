using System;
using ee.itcollege.nikita.Contracts.BLL.Base.Mappers;
using internalDTO = DAL.App.DTO.DomainLikeDTO;
using externalDTO = BLL.App.DTO.DomainLikeDTO;


namespace BLL.App.Mappers
{
    public class ProductInCategoryMapper : IBaseBLLMapper
    {
        public TOutObject Map<TOutObject>(object inObject) where TOutObject : class
        {
            if (typeof(TOutObject) == typeof(externalDTO.ProductInCategory))
            {
                return MapFromDAL((internalDTO.ProductInCategory) inObject) as TOutObject;
            }

            if (typeof(TOutObject) == typeof(internalDTO.ProductInCategory))
            {
                return MapFromBLL((externalDTO.ProductInCategory) inObject) as TOutObject;
            }

            throw new InvalidCastException($"No conversion from {inObject.GetType().FullName} to {typeof(TOutObject).FullName}");
        
        }
        
        public static externalDTO.ProductInCategory MapFromDAL(internalDTO.ProductInCategory productInCategory)
        {
            var res = productInCategory == null ? null : new externalDTO.ProductInCategory
            {
                Id = productInCategory.Id,
                ProductId = productInCategory.ProductId,
                //Product = ProductMapper.MapFromDAL(productInCategory.Product),
                CategoryId = productInCategory.CategoryId,
                //Category = CategoryMapper.MapFromDAL(productInCategory.Category)
            };
            
            if (productInCategory?.Product != null)
            {
                res.Product = new BLL.App.DTO.DomainLikeDTO.Product()
                {
                    ProductName = productInCategory.Product.ProductName
                };
            }
            
            if (productInCategory?.Category != null)
            {
                res.Category = new BLL.App.DTO.DomainLikeDTO.Category()
                {
                    CategoryName = productInCategory.Category.CategoryName
                };
            }

            return res;
        }
        
        public static BLL.App.DTO.IdAndNameDTO.ProductIdName MapFromDAL(DAL.App.DTO.IdAndNameDTO.ProductIdName productIdName)
        {
            var res = productIdName == null ? null : new BLL.App.DTO.IdAndNameDTO.ProductIdName
            {
                Id = productIdName.Id,
                ProductName = productIdName.ProductName,
                ProductInCategoryId = productIdName.ProductInCategoryId
            };
            return res;
        }

        public static internalDTO.ProductInCategory MapFromBLL(externalDTO.ProductInCategory productInCategory)
        {
            var res = productInCategory == null ? null : new internalDTO.ProductInCategory
            {
                Id = productInCategory.Id,
                ProductId = productInCategory.ProductId,
                Product = ProductMapper.MapFromBLL(productInCategory.Product),
                CategoryId = productInCategory.CategoryId,
                Category = CategoryMapper.MapFromBLL(productInCategory.Category)
            };

            return res;
        }
        
        public static BLL.App.DTO.ProductInCategory MapFromDAL(DAL.App.DTO.ProductInCategory productInCategory)
        {
            var res = productInCategory == null ? null : new BLL.App.DTO.ProductInCategory
            {
                Id = productInCategory.Id,
                ProductId = productInCategory.ProductId,
                ProductName = productInCategory.ProductName,
                CategoryId = productInCategory.CategoryId,
                CategoryName = productInCategory.CategoryName
            };

            return res;
        }
    }
}