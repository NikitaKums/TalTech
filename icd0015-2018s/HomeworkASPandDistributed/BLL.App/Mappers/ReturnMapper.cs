using System;
using System.Linq;
using BLL.App.DTO.IdAndNameDTO;
using ee.itcollege.nikita.Contracts.BLL.Base.Mappers;
using internalDTO = DAL.App.DTO.DomainLikeDTO;
using externalDTO = BLL.App.DTO.DomainLikeDTO;

namespace BLL.App.Mappers
{
    public class ReturnMapper : IBaseBLLMapper
    {
        public TOutObject Map<TOutObject>(object inObject) where TOutObject : class
        {
            if (typeof(TOutObject) == typeof(externalDTO.Return))
            {
                return MapFromDAL((internalDTO.Return) inObject) as TOutObject;
            }

            if (typeof(TOutObject) == typeof(internalDTO.Return))
            {
                return MapFromBLL((externalDTO.Return) inObject) as TOutObject;
            }

            throw new InvalidCastException($"No conversion from {inObject.GetType().FullName} to {typeof(TOutObject).FullName}");
        }
        
        public static externalDTO.Return MapFromDAL(internalDTO.Return @return)
        {
            var res = @return == null ? null : new externalDTO.Return
            {
                Id = @return.Id,
                Description = @return.Description,
                ShopId = @return.ShopId,
                Shop = ShopMapper.MapFromDAL(@return.Shop)
            };
            
            if (@return?.ProductsReturned != null)
            {
                res.ProductsReturned = @return.ProductsReturned.Select(e => ProductReturnedMapper.MapFromDAL(e)).ToList();
            }

            return res;
        }

        public static internalDTO.Return MapFromBLL(externalDTO.Return returnWithProductCount)
        {
            var res = returnWithProductCount == null ? null : new internalDTO.Return
            {
                Id = returnWithProductCount.Id,
                Description = returnWithProductCount.Description,
                ShopId = returnWithProductCount.ShopId,
                Shop = ShopMapper.MapFromBLL(returnWithProductCount.Shop)
            };

            return res;
        }
        
        public static BLL.App.DTO.IdAndNameDTO.ReturnIdName MapFromDAL(DAL.App.DTO.IdAndNameDTO.ReturnIdName returnIdName)
        {
            var res = returnIdName == null ? null : new BLL.App.DTO.IdAndNameDTO.ReturnIdName
            {
                Id = returnIdName.Id,
                ReturnDescription = returnIdName.ReturnDescription
            };

            return res;
        }
        
        public static BLL.App.DTO.ReturnWithProductCount MapFromDAL(DAL.App.DTO.ReturnWithProductCount @return)
        {
            var res = @return == null ? null : new BLL.App.DTO.ReturnWithProductCount
            {
                Id = @return.Id,
                Description = @return.Description,
                ShopId = @return.ShopId,
                ShopName = @return.ShopName,
                ProductsReturnedCount = @return.ProductsReturnedCount
            };

            if (@return?.ProductIdNameDtos != null)
            {
                res.ProductIdNameDtos = @return.ProductIdNameDtos.Select(e => new ProductIdName
                {
                    Id = e.Id,
                    ProductName = e.ProductName
                }).ToList();
            }

            return res;
        }
    }
}