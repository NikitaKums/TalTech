using System;
using System.Linq;
using Domain;
using ee.itcollege.nikita.Contracts.DAL.Base.Mappers;

namespace DAL.App.EF.Mappers
{
    public class SaleMapper : IBaseDALMapper
    {
        public TOutObject Map<TOutObject>(object inObject) where TOutObject : class
        {
            if (typeof(TOutObject) == typeof(DAL.App.DTO.DomainLikeDTO.Sale))
            {
                return MapFromDomain((Domain.Sale) inObject) as TOutObject;
            }

            if (typeof(TOutObject) == typeof(Domain.Sale))
            {
                return MapFromDAL((DAL.App.DTO.DomainLikeDTO.Sale) inObject) as TOutObject;
            }

            throw new InvalidCastException($"No conversion from {inObject.GetType().FullName} to {typeof(TOutObject).FullName}");
        }
        
        public static DAL.App.DTO.DomainLikeDTO.Sale MapFromDomain(Domain.Sale sale)
        {
            var res = sale == null ? null : new DAL.App.DTO.DomainLikeDTO.Sale
            {
                Id = sale.Id,
                Description = sale.Description.Translate(),
                AppUserId = sale.AppUserId,
                AppUser = AppUserMapper.MapFromDomain(sale.AppUser),
                SaleInitialCreationTime = sale.SaleInitialCreationTime,
                
            };

            if (sale?.ProductsSold != null)
            {
                res.ProductsSold = sale.ProductsSold.Select(e => ProductSoldMapper.MapFromDomain(e)).ToList();
                res.AllTotalSaleAmount = sale.ProductsSold.Sum(e => e.Quantity * e.Product.SellPrice);
                res.TodayTotalSaleAmount = sale.ProductsSold.Where(e => DateTime.Today < e.ProductSoldTime && e.ProductSoldTime < DateTime.Today.AddDays(1).AddTicks(-1))
                    .Sum(e => e.Quantity * e.Product.SellPrice);
            }
            
            return res;
        }

        public static Domain.Sale MapFromDAL(DAL.App.DTO.DomainLikeDTO.Sale sale)
        {
            var res = sale == null ? null : new Domain.Sale
            {
                Id = sale.Id,
                Description = new MultiLangString(sale.Description),
                AppUserId = sale.AppUserId,
                AppUser = AppUserMapper.MapFromDAL(sale.AppUser),
                SaleInitialCreationTime = sale.SaleInitialCreationTime,
            };

            return res;
        }
    }
}