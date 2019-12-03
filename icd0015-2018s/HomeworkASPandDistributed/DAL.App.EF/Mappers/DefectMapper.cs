using System;
using System.Linq;
using DAL.App.DTO.DomainLikeDTO;
using Domain;
using ee.itcollege.nikita.Contracts.DAL.Base.Mappers;

namespace DAL.App.EF.Mappers
{
    public class DefectMapper : IBaseDALMapper
    {
        public TOutObject Map<TOutObject>(object inObject) where TOutObject : class
        {
            if (typeof(TOutObject) == typeof(DAL.App.DTO.DomainLikeDTO.Defect))
            {
                return MapFromDomain((Domain.Defect) inObject) as TOutObject;
            }

            if (typeof(TOutObject) == typeof(Domain.Defect))
            {
                return MapFromDAL((DAL.App.DTO.DomainLikeDTO.Defect) inObject) as TOutObject;
            }

            throw new InvalidCastException($"No conversion from {inObject.GetType().FullName} to {typeof(TOutObject).FullName}");
        }
        
        public static DAL.App.DTO.DomainLikeDTO.Defect MapFromDomain(Domain.Defect defect)
        {
            var res = defect == null ? null : new DAL.App.DTO.DomainLikeDTO.Defect
                {
                    Id = defect.Id,
                    Description = defect.Description.Translate(),
                    ShopId = defect.ShopId,
                    Shop = ShopMapper.MapFromDomain(defect.Shop)
                };
            
            if (defect?.ProductsWithDefect != null)
            {
                res.ProductsWithDefect = defect.ProductsWithDefect.Select(e => ProductWithDefectMapper.MapFromDomain(e)).ToList();
            }

            return res;
        }

        public static Domain.Defect MapFromDAL(DAL.App.DTO.DomainLikeDTO.Defect defectWithProductCount)
        {
            var res = defectWithProductCount == null ? null : new Domain.Defect
            {
                Id = defectWithProductCount.Id,
                Description = new MultiLangString(defectWithProductCount.Description),
                ShopId = defectWithProductCount.ShopId,
                Shop = ShopMapper.MapFromDAL(defectWithProductCount.Shop)
            };

            return res;
        }
    }
}