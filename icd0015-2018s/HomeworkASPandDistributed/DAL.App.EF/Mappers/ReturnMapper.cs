using System;
using System.Linq;
using Domain;
using ee.itcollege.nikita.Contracts.DAL.Base.Mappers;

namespace DAL.App.EF.Mappers
{
    public class ReturnMapper : IBaseDALMapper
    {
        public TOutObject Map<TOutObject>(object inObject) where TOutObject : class
        {
            if (typeof(TOutObject) == typeof(DAL.App.DTO.DomainLikeDTO.Return))
            {
                return MapFromDomain((Domain.Return) inObject) as TOutObject;
            }

            if (typeof(TOutObject) == typeof(Domain.Return))
            {
                return MapFromDAL((DAL.App.DTO.DomainLikeDTO.Return) inObject) as TOutObject;
            }

            throw new InvalidCastException($"No conversion from {inObject.GetType().FullName} to {typeof(TOutObject).FullName}");
        }
        
        public static DAL.App.DTO.DomainLikeDTO.Return MapFromDomain(Domain.Return @return)
        {
            var res = @return == null ? null : new DAL.App.DTO.DomainLikeDTO.Return
            {
                Id = @return.Id,
                Description = @return.Description.Translate(),
                ShopId = @return.ShopId,
                Shop = ShopMapper.MapFromDomain(@return.Shop)
            };
            
            if (@return?.ProductsReturned != null)
            {
                res.ProductsReturned = @return.ProductsReturned.Select(e => ProductReturnedMapper.MapFromDomain(e)).ToList();
            }

            return res;
        }

        public static Domain.Return MapFromDAL(DAL.App.DTO.DomainLikeDTO.Return returnWithProductCount)
        {
            var res = returnWithProductCount == null ? null : new Domain.Return
            {
                Id = returnWithProductCount.Id,
                Description = new MultiLangString(returnWithProductCount.Description),
                ShopId = returnWithProductCount.ShopId,
                Shop = ShopMapper.MapFromDAL(returnWithProductCount.Shop)
            };

            return res;
        }
    }
}