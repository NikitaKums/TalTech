using System;
using System.Linq;
using ee.itcollege.nikita.Contracts.BLL.Base.Mappers;
using internalDTO = DAL.App.DTO.DomainLikeDTO;
using externalDTO = BLL.App.DTO.DomainLikeDTO;

namespace BLL.App.Mappers
{
    public class SaleMapper : IBaseBLLMapper
    {
        public TOutObject Map<TOutObject>(object inObject) where TOutObject : class
        {
            if (typeof(TOutObject) == typeof(externalDTO.Sale))
            {
                return MapFromDAL((internalDTO.Sale) inObject) as TOutObject;
            }

            if (typeof(TOutObject) == typeof(internalDTO.Sale))
            {
                return MapFromBLL((externalDTO.Sale) inObject) as TOutObject;
            }

            throw new InvalidCastException($"No conversion from {inObject.GetType().FullName} to {typeof(TOutObject).FullName}");
        }
        
        public static externalDTO.Sale MapFromDAL(internalDTO.Sale sale)
        {
            var res = sale == null ? null : new externalDTO.Sale
            {
                Id = sale.Id,
                Description = sale.Description,
                AppUserId = sale.AppUserId,
                AppUser = AppUserMapper.MapFromDAL(sale.AppUser),
                SaleInitialCreationTime = sale.SaleInitialCreationTime,
            };
            
            if (sale?.ProductsSold != null)
            {
                res.ProductsSold = sale.ProductsSold.Select(e => ProductSoldMapper.MapFromDAL(e)).ToList();
                res.AllTotalSaleAmount = sale.ProductsSold.Sum(e => e.Quantity * e.Product.SellPrice);
                res.TodayTotalSaleAmount = sale.ProductsSold.Where(e => DateTime.Today < e.ProductSoldTime && e.ProductSoldTime < DateTime.Today.AddDays(1).AddTicks(-1))
                    .Sum(e => e.Quantity * e.Product.SellPrice);
            }

            return res;
        }

        public static internalDTO.Sale MapFromBLL(externalDTO.Sale sale)
        {
            var res = sale == null ? null : new internalDTO.Sale
            {
                Id = sale.Id,
                Description = sale.Description,
                AppUserId = sale.AppUserId,
                AppUser = AppUserMapper.MapFromBLL(sale.AppUser),
                SaleInitialCreationTime = sale.SaleInitialCreationTime
            };

            return res;
        }
        
        public static BLL.App.DTO.SaleWithProductCount MapFromDAL(DAL.App.DTO.SaleWithProductCount sale)
        {
            var res = sale == null ? null : new BLL.App.DTO.SaleWithProductCount
            {
                Id = sale.Id,
                Description = sale.Description,
                AppUserId = sale.AppUserId,
                AppUserName = sale.AppUserName,
                AppUserLastName = sale.AppUserLastName,
                ProductsSoldCount = sale.ProductsSoldCount,
                SaleInitialCreationTime = sale.SaleInitialCreationTime,
                TodayTotalSaleAmount = sale.TodayTotalSaleAmount,
                AllTotalSaleAmount = sale.AllTotalSaleAmount
            };
            
            if (sale?.ProductsInSaleDTOs != null)
            {
                res.ProductsInSaleDTOs = sale.ProductsInSaleDTOs.Select(e => new BLL.App.DTO.IdAndNameDTO.ProductSoldProductSaleIdName()
                {
                    Id = e.Id,
                    ProductName = e.ProductName
                }).ToList();
            }

            return res;
        }
        
    }
}