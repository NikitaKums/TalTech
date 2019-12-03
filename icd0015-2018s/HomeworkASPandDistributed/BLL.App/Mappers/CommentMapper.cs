using System;
using ee.itcollege.nikita.Contracts.BLL.Base.Mappers;
using internalDTO = DAL.App.DTO;
using externalDTO = BLL.App.DTO;

namespace BLL.App.Mappers
{
    public class CommentMapper : IBaseBLLMapper
    {
        public TOutObject Map<TOutObject>(object inObject) where TOutObject : class
        {
            if (typeof(TOutObject) == typeof(externalDTO.DomainLikeDTO.Comment))
            {
                return MapFromDAL((internalDTO.DomainLikeDTO.Comment) inObject) as TOutObject;
            }

            if (typeof(TOutObject) == typeof(internalDTO.DomainLikeDTO.Comment))
            {
                return MapFromBLL((externalDTO.DomainLikeDTO.Comment) inObject) as TOutObject;
            }

            throw new InvalidCastException($"No conversion from {inObject.GetType().FullName} to {typeof(TOutObject).FullName}");
        }
        
        public static externalDTO.DomainLikeDTO.Comment MapFromDAL(internalDTO.DomainLikeDTO.Comment comment)
        {
            var res = comment == null ? null : new externalDTO.DomainLikeDTO.Comment
            {
                Id = comment.Id,
                CommentTitle = comment.CommentTitle,
                CommentBody = comment.CommentBody,
                ProductId = comment.ProductId,
                Product = ProductMapper.MapFromDAL(comment.Product),
                ShopId = comment.ShopId,
                Shop = ShopMapper.MapFromDAL(comment.Shop)
            };
            
            return res;
        }

        public static internalDTO.DomainLikeDTO.Comment MapFromBLL(externalDTO.DomainLikeDTO.Comment comment)
        {
            var res = comment == null ? null : new internalDTO.DomainLikeDTO.Comment
            {
                Id = comment.Id,
                CommentTitle = comment.CommentTitle,
                CommentBody = comment.CommentBody,
                ProductId = comment.ProductId,
                Product = ProductMapper.MapFromBLL(comment.Product),
                ShopId = comment.ShopId,
                Shop = ShopMapper.MapFromBLL(comment.Shop)
            };
            
            return res;
        }
        
        public static externalDTO.Comment MapFromDAL(internalDTO.Comment comment)
        {
            var res = comment == null ? null : new externalDTO.Comment
            {
                Id = comment.Id,
                CommentTitle = comment.CommentTitle,
                CommentBody = comment.CommentBody,
                ProductId = comment.ProductId,
                ShopId = comment.ShopId,
                ProductName = comment.ProductName
            };
            
            return res;
        }
        
        public static BLL.App.DTO.IdAndNameDTO.CommentIdTitleBody MapFromDAL(DAL.App.DTO.IdAndNameDTO.CommentIdTitleBody comment)
        {
            var res = comment == null ? null : new BLL.App.DTO.IdAndNameDTO.CommentIdTitleBody
            {
                Id = comment.Id,
                CommentTitle = comment.CommentTitle,
                CommentBody = comment.CommentBody
            };
            return res;
        }
    }
}