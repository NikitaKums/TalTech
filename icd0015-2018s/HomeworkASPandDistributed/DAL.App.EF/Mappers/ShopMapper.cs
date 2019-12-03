using System;
using Domain;
using ee.itcollege.nikita.Contracts.DAL.Base.Mappers;

namespace DAL.App.EF.Mappers
{
    public class ShopMapper : IBaseDALMapper
    {
        public TOutObject Map<TOutObject>(object inObject) where TOutObject : class
        {
            if (typeof(TOutObject) == typeof(DAL.App.DTO.DomainLikeDTO.Shop))
            {
                return MapFromDomain((Domain.Shop) inObject) as TOutObject;
            }

            if (typeof(TOutObject) == typeof(Domain.Shop))
            {
                return MapFromDAL((DAL.App.DTO.DomainLikeDTO.Shop) inObject) as TOutObject;
            }

            throw new InvalidCastException($"No conversion from {inObject.GetType().FullName} to {typeof(TOutObject).FullName}");
        }
        
        public static DAL.App.DTO.DomainLikeDTO.Shop MapFromDomain(Domain.Shop shop)
        {
            var res = shop == null ? null : new DAL.App.DTO.DomainLikeDTO.Shop
            {
                Id = shop.Id,
                ShopName = shop.ShopName?.Translate(),
                ShopAddress = shop.ShopAddress?.Translate(),
                ShopContact = shop.ShopContact?.Translate(),
                ShopContact2 = shop.ShopContact2?.Translate()
            };

            return res;
        }

        public static Domain.Shop MapFromDAL(DAL.App.DTO.DomainLikeDTO.Shop shop)
        {
            var res = shop == null ? null : new Domain.Shop
            {
                Id = shop.Id,
                ShopName = new MultiLangString(shop.ShopName),
                ShopAddress = new MultiLangString(shop.ShopAddress),
                ShopContact = new MultiLangString(shop.ShopContact),
                ShopContact2 = new MultiLangString(shop.ShopContact2)
            };

            return res;
        }
    }
}