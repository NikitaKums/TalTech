using System;
using System.Linq;
using externalDTO = PublicApi.v1.DTO;
using internalDTO = BLL.App.DTO;


namespace PublicApi.v1.Mappers
{
    public class SaleMapper
    {
        public TOutObject Map<TOutObject>(object inObject)
            where TOutObject : class
        {
            if (typeof(TOutObject) == typeof(externalDTO.Sale))
            {
                // map internal to external
                return MapFromBLL((internalDTO.SaleWithProductCount) inObject) as TOutObject;
            }

            if (typeof(TOutObject) == typeof(internalDTO.SaleWithProductCount))
            {
                // map external to internal
                return MapFromExternal((externalDTO.Sale) inObject) as TOutObject;
            }

            throw new InvalidCastException($"No conversion from {inObject.GetType().FullName} to {typeof(TOutObject).FullName}");
        }


        public static externalDTO.Sale MapFromBLL(internalDTO.SaleWithProductCount sale)
        {
            var res = sale == null ? null : new externalDTO.Sale()
            {
                Id = sale.Id,
                AppUserId = sale.AppUserId,
                Description = sale.Description,
                ProductsSoldCount = sale.ProductsSoldCount,
                SaleInitialCreationTime = sale.SaleInitialCreationTime,
                AppUserName = sale.AppUserName,
                AppUserLastName = sale.AppUserLastName,
                TodayTotalSaleAmount = sale.TodayTotalSaleAmount,
                AllTotalSaleAmount = sale.AllTotalSaleAmount
            };

            if (sale?.ProductsInSaleDTOs != null)
            {
                res.ProductsInSaleDTOs = sale.ProductsInSaleDTOs.Select(e => ProductSoldMapper.MapFromBLL(e)).ToList();
            }
            return res;
        }
        
        public static externalDTO.Sale MapFromBLL(internalDTO.DomainLikeDTO.Sale sale)
        {
            var res = sale == null ? null : new externalDTO.Sale()
            {
                Id = sale.Id,
                AppUserId = sale.AppUserId,
                Description = sale.Description,
                SaleInitialCreationTime = sale.SaleInitialCreationTime,
            };
            
            return res;
        }
        
        public static externalDTO.IdAndNameDTO.SaleIdName MapFromBLLIdName(internalDTO.SaleWithProductCount sale)
        {
            var res = sale == null ? null : new externalDTO.IdAndNameDTO.SaleIdName() 
            {
                Id = sale.Id,
                SaleDescription = sale.Description
            };
            return res;
        }
        
        public static internalDTO.DomainLikeDTO.Sale MapFromExternal(externalDTO.Sale sale)
        {
            var res = sale == null ? null : new internalDTO.DomainLikeDTO.Sale()
            {
                Id = sale.Id,
                AppUserId = sale.AppUserId,
                Description = sale.Description,
                SaleInitialCreationTime = sale.SaleInitialCreationTime,
            };
            
            /*if (sale?.ProductsInSaleDTOs != null)
            {
                res.ProductsInSaleDTOs = sale.ProductsInSaleDTOs.Select(e => ProductSoldMapper.MapFromBLL(e)).ToList();
            }*/
            return res;
        }

        
    }
}