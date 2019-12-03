using System;
using System.Linq;
using ee.itcollege.nikita.Contracts.BLL.Base.Mappers;
using internalDTO = DAL.App.DTO.DomainLikeDTO;
using externalDTO = BLL.App.DTO.DomainLikeDTO;

namespace BLL.App.Mappers
{
    public class DefectMapper : IBaseBLLMapper
    {
        public TOutObject Map<TOutObject>(object inObject) where TOutObject : class
        {
            if (typeof(TOutObject) == typeof(externalDTO.Defect))
            {
                return MapFromDAL((internalDTO.Defect) inObject) as TOutObject;
            }

            if (typeof(TOutObject) == typeof(internalDTO.Defect))
            {
                return MapFromBLL((externalDTO.Defect) inObject) as TOutObject;
            }

            throw new InvalidCastException($"No conversion from {inObject.GetType().FullName} to {typeof(TOutObject).FullName}");
        }
        
        public static externalDTO.Defect MapFromDAL(internalDTO.Defect defect)
        {
            var res = defect == null ? null : new externalDTO.Defect
            {
                Id = defect.Id,
                Description = defect.Description,
                ShopId = defect.ShopId,
                Shop = ShopMapper.MapFromDAL(defect.Shop)
            };
            
            if (defect?.ProductsWithDefect != null)
            {
                res.ProductsWithDefect = defect.ProductsWithDefect.Select(e => ProductWithDefectMapper.MapFromDAL(e)).ToList();
            }

            return res;
        }

        public static internalDTO.Defect MapFromBLL(externalDTO.Defect defectWithProductCount)
        {
            var res = defectWithProductCount == null ? null : new internalDTO.Defect
            {
                Id = defectWithProductCount.Id,
                Description = defectWithProductCount.Description,
                ShopId = defectWithProductCount.ShopId,
                Shop = ShopMapper.MapFromBLL(defectWithProductCount.Shop)
            };

            return res;
        }
        
        public static BLL.App.DTO.DefectWithProductCount MapFromDAL(DAL.App.DTO.DefectWithProductCount defect)
        {
            var res = defect == null ? null : new BLL.App.DTO.DefectWithProductCount
            {
                Id = defect.Id,
                Description = defect.Description,
                ShopId = defect.ShopId,
                ShopName = defect.ShopName,
                ProductsWithDefectCount = defect.ProductsWithDefectCount
            };

            return res;
        }
    }
}