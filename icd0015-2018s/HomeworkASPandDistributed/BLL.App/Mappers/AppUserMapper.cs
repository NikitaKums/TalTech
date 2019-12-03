using System;
using ee.itcollege.nikita.Contracts.BLL.Base.Mappers;
using internalDTO = DAL.App.DTO;
using externalDTO = BLL.App.DTO;

namespace BLL.App.Mappers
{
    public class AppUserMapper : IBaseBLLMapper
    {
        public TOutObject Map<TOutObject>(object inObject)
            where TOutObject : class
        {
            if (typeof(TOutObject) == typeof(BLL.App.DTO.DomainLikeDTO.Identity.AppUser))
            {
                return MapFromDAL((internalDTO.DomainLikeDTO.Identity.AppUser) inObject) as TOutObject;
            }

            if (typeof(TOutObject) == typeof(DAL.App.DTO.DomainLikeDTO.Identity.AppUser))
            {
                return MapFromBLL((externalDTO.DomainLikeDTO.Identity.AppUser) inObject) as TOutObject;
            }

            throw new InvalidCastException($"No conversion from {inObject.GetType().FullName} to {typeof(TOutObject).FullName}");
        }


        public static BLL.App.DTO.DomainLikeDTO.Identity.AppUser MapFromDAL(DAL.App.DTO.DomainLikeDTO.Identity.AppUser appUser)
        {
            var res = appUser == null ? null : new BLL.App.DTO.DomainLikeDTO.Identity.AppUser
            {
               Id = appUser.Id,
               FirstName = appUser.FirstName,
               LastName = appUser.LastName,
               Email = appUser.Email,
               Aadress = appUser.Aadress,
               ShopId = appUser.ShopId,
               Shop = ShopMapper.MapFromDAL(appUser.Shop)
            };
            return res;
        }
        
        public static DAL.App.DTO.DomainLikeDTO.Identity.AppUser MapFromBLL(BLL.App.DTO.DomainLikeDTO.Identity.AppUser appUser)
        {
            var res = appUser == null ? null : new DAL.App.DTO.DomainLikeDTO.Identity.AppUser
            {
                Id = appUser.Id,
                FirstName = appUser.FirstName,
                LastName = appUser.LastName,
                Aadress = appUser.Aadress,
                Email = appUser.Email,
                ShopId = appUser.ShopId,
                Shop = ShopMapper.MapFromBLL(appUser.Shop)
            };
            
            return res;
        }
        
        public static BLL.App.DTO.Identity.AppUser MapFromDAL(DAL.App.DTO.Identity.AppUser appUser)
        {
            var res = appUser == null ? null : new BLL.App.DTO.Identity.AppUser
            {
                Id = appUser.Id,
                FirstName = appUser.FirstName,
                LastName = appUser.LastName,
                Address = appUser.Address,
                ShopId = appUser.ShopId,
                ShopName = appUser.ShopName
            };
            return res;
        }
    }
}