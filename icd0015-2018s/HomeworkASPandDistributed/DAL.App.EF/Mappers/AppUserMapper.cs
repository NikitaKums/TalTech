using System;
using DAL.App.DTO.DomainLikeDTO.Identity;
using ee.itcollege.nikita.Contracts.DAL.Base.Mappers;

namespace DAL.App.EF.Mappers
{
    public class AppUserMapper : IBaseDALMapper
    {
        public TOutObject Map<TOutObject>(object inObject) where TOutObject : class
        {
            if (typeof(TOutObject) == typeof(DAL.App.DTO.DomainLikeDTO.Identity.AppUser))
            {
                return MapFromDomain((Domain.Identity.AppUser) inObject) as TOutObject;
            }

            if (typeof(TOutObject) == typeof(Domain.Identity.AppUser))
            {
                return MapFromDAL((DAL.App.DTO.DomainLikeDTO.Identity.AppUser) inObject) as TOutObject;
            }

            throw new InvalidCastException($"No conversion from {inObject.GetType().FullName} to {typeof(TOutObject).FullName}");
        }
        
        public static DAL.App.DTO.DomainLikeDTO.Identity.AppUser MapFromDomain(Domain.Identity.AppUser appUser)
        {
            var res = appUser == null ? null : new DAL.App.DTO.DomainLikeDTO.Identity.AppUser
            {
                Id = appUser.Id,
                FirstName = appUser.FirstName,
                LastName = appUser.LastName,
                Email = appUser.Email,
                Aadress = appUser.Aadress,
                ShopId = appUser.ShopId,
                Shop = ShopMapper.MapFromDomain(appUser.Shop)
            };

            return res;
        }

        public static Domain.Identity.AppUser MapFromDAL(DAL.App.DTO.DomainLikeDTO.Identity.AppUser appUser)
        {
            var res = appUser == null ? null : new Domain.Identity.AppUser
            {
                Id = appUser.Id,
                FirstName = appUser.FirstName,
                LastName = appUser.LastName,
                Aadress = appUser.Aadress,
                Email = appUser.Email,
                ShopId = appUser.ShopId,
                Shop = ShopMapper.MapFromDAL(appUser.Shop)
            };

            return res;
        }

    }
}