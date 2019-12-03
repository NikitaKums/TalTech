using System;
using internalDTO = BLL.App.DTO;
using externalDTO = PublicApi.v1.DTO;

namespace PublicApi.v1.Mappers
{
    public class AppUserMapper
    {
        public TOutObject Map<TOutObject>(object inObject)
            where TOutObject : class
        {
            if (typeof(TOutObject) == typeof(externalDTO.AppUser))
            {
                return MapFromBLL((internalDTO.Identity.AppUser) inObject) as TOutObject;
            }

            if (typeof(TOutObject) == typeof(internalDTO.Identity.AppUser))
            {
                return MapFromExternal((externalDTO.AppUser) inObject) as TOutObject;
            }

            throw new InvalidCastException(
                $"No conversion from {inObject.GetType().FullName} to {typeof(TOutObject).FullName}");
        }
        
        public static internalDTO.Identity.AppUser MapFromExternal(externalDTO.AppUser appUser)
        {
            var res = appUser == null ? null : new internalDTO.Identity.AppUser()
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
        
        public static externalDTO.AppUser MapFromBLL(internalDTO.Identity.AppUser appUser)
        {
            var res = appUser == null ? null : new externalDTO.AppUser()
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
        
        public static externalDTO.AppUserShop MapFromBLL(internalDTO.Identity.AppUserShop appUser)
        {
            var res = appUser == null ? null : new externalDTO.AppUserShop()
            {
                ShopId = appUser.ShopId,
                ShopName = appUser.ShopName
            };
            return res;
        }

    }
}