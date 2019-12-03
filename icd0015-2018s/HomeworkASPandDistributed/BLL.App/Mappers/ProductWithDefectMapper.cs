using System;
using ee.itcollege.nikita.Contracts.BLL.Base.Mappers;
using internalDTO = DAL.App.DTO.DomainLikeDTO;
using externalDTO = BLL.App.DTO.DomainLikeDTO;

namespace BLL.App.Mappers
{
    public class ProductWithDefectMapper : IBaseBLLMapper
    {
        public TOutObject Map<TOutObject>(object inObject) where TOutObject : class
        {
            if (typeof(TOutObject) == typeof(externalDTO.ProductWithDefect))
            {
                return MapFromDAL((internalDTO.ProductWithDefect) inObject) as TOutObject;
            }

            if (typeof(TOutObject) == typeof(internalDTO.ProductWithDefect))
            {
                return MapFromBLL((externalDTO.ProductWithDefect) inObject) as TOutObject;
            }

            throw new InvalidCastException($"No conversion from {inObject.GetType().FullName} to {typeof(TOutObject).FullName}");
        }
        
        public static externalDTO.ProductWithDefect MapFromDAL(internalDTO.ProductWithDefect productWithDefect)
        {
            var res = productWithDefect == null ? null : new externalDTO.ProductWithDefect
                {
                    Id = productWithDefect.Id,
                    ProductId = productWithDefect.ProductId,
                    //Product = ProductMapper.MapFromDAL(productWithDefect.Product),
                    DefectRecordingTime = productWithDefect.DefectRecordingTime,
                    Quantity = productWithDefect.Quantity,
                    DefectId = productWithDefect.DefectId,
                    //Defect = DefectMapper.MapFromDAL(productWithDefect.Defect)
                };
            
            if (productWithDefect?.Product != null)
            {
                res.Product = new BLL.App.DTO.DomainLikeDTO.Product()
                {
                    ProductName = productWithDefect.Product.ProductName
                };
            }
            
            if (productWithDefect?.Defect != null)
            {
                res.Defect = new BLL.App.DTO.DomainLikeDTO.Defect()
                {
                    Description = productWithDefect.Defect.Description
                };
            }
            
            return res;
        }
        
        public static BLL.App.DTO.IdAndNameDTO.ProductIdName MapFromDAL(DAL.App.DTO.IdAndNameDTO.ProductIdName productIdName)
        {
            var res = productIdName == null ? null : new BLL.App.DTO.IdAndNameDTO.ProductIdName
            {
                Id = productIdName.Id,
                ProductName = productIdName.ProductName,
                ProductWithDefectId = productIdName.ProductWithDefectId
            };
            return res;
        }

        public static internalDTO.ProductWithDefect MapFromBLL(externalDTO.ProductWithDefect productWithDefect)
        {
            var res = productWithDefect == null ? null : new internalDTO.ProductWithDefect
            {
                Id = productWithDefect.Id,
                ProductId = productWithDefect.ProductId,
                Product = ProductMapper.MapFromBLL(productWithDefect.Product),
                DefectRecordingTime = productWithDefect.DefectRecordingTime,
                Quantity = productWithDefect.Quantity,
                DefectId = productWithDefect.DefectId,
                Defect = DefectMapper.MapFromBLL(productWithDefect.Defect)
            };
            return res;
        }
        
        public static BLL.App.DTO.ProductWithDefect MapFromDAL(DAL.App.DTO.ProductWithDefect productWithDefect)
        {
            var res = productWithDefect == null ? null : new BLL.App.DTO.ProductWithDefect
            {
                Id = productWithDefect.Id,
                ProductId = productWithDefect.ProductId,
                ProductName = productWithDefect.ProductName,
                DefectRecordingTime = productWithDefect.DefectRecordingTime,
                Quantity = productWithDefect.Quantity,
                DefectId = productWithDefect.DefectId,
                DefectDescription = productWithDefect.DefectDescription,
            };
            return res;
        }
    }
}