using System;
using ee.itcollege.nikita.Contracts.DAL.Base.Mappers;

namespace DAL.App.EF.Mappers
{
    public class ProductWithDefectMapper : IBaseDALMapper
    {
        public TOutObject Map<TOutObject>(object inObject) where TOutObject : class
        {
            if (typeof(TOutObject) == typeof(DAL.App.DTO.DomainLikeDTO.ProductWithDefect))
            {
                return MapFromDomain((Domain.ProductWithDefect) inObject) as TOutObject;
            }

            if (typeof(TOutObject) == typeof(Domain.ProductWithDefect))
            {
                return MapFromDAL((DAL.App.DTO.DomainLikeDTO.ProductWithDefect) inObject) as TOutObject;
            }

            throw new InvalidCastException($"No conversion from {inObject.GetType().FullName} to {typeof(TOutObject).FullName}");
        }
        
        public static DAL.App.DTO.DomainLikeDTO.ProductWithDefect MapFromDomain(Domain.ProductWithDefect productWithDefect)
        {
            var res = productWithDefect == null ? null : new DAL.App.DTO.DomainLikeDTO.ProductWithDefect
                {
                    Id = productWithDefect.Id,
                    ProductId = productWithDefect.ProductId,
                    //Product = ProductMapper.MapFromDomain(productWithDefect.Product),
                    DefectRecordingTime = productWithDefect.DefectRecordingTime,
                    Quantity = productWithDefect.Quantity,
                    DefectId = productWithDefect.DefectId,
                    //Defect = DefectMapper.MapFromDomain(productWithDefect.Defect)
                };
            
            if (productWithDefect?.Product != null)
            {
                res.Product = new DAL.App.DTO.DomainLikeDTO.Product()
                {
                    ProductName = productWithDefect.Product.ProductName.Translate()
                };
            }
            
            if (productWithDefect?.Defect != null)
            {
                res.Defect = new DAL.App.DTO.DomainLikeDTO.Defect()
                {
                    Description = productWithDefect.Defect.Description.Translate()
                };
            }
            
            return res;
        }

        public static Domain.ProductWithDefect MapFromDAL(DAL.App.DTO.DomainLikeDTO.ProductWithDefect productWithDefect)
        {
            var res = productWithDefect == null ? null : new Domain.ProductWithDefect
            {
                Id = productWithDefect.Id,
                ProductId = productWithDefect.ProductId,
                Product = ProductMapper.MapFromDAL(productWithDefect.Product),
                DefectRecordingTime = productWithDefect.DefectRecordingTime,
                Quantity = productWithDefect.Quantity,
                DefectId = productWithDefect.DefectId,
                Defect = DefectMapper.MapFromDAL(productWithDefect.Defect)
            };
            return res;
        }
    }
}