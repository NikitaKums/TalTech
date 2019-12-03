using System;
using externalDTO = PublicApi.v1.DTO;
using internalDTO = BLL.App.DTO;

namespace PublicApi.v1.Mappers
{
    public class CommentMapper
    {
        public TOutObject Map<TOutObject>(object inObject) where TOutObject : class
        {
            if (typeof(TOutObject) == typeof(externalDTO.Comment))
            {
                return MapFromBLL((internalDTO.Comment) inObject) as TOutObject;
            }

            if (typeof(TOutObject) == typeof(internalDTO.DomainLikeDTO.Comment))
            {
                return MapFromExternal((externalDTO.Comment) inObject) as TOutObject;
            }

            throw new InvalidCastException($"No conversion from {inObject.GetType().FullName} to {typeof(TOutObject).FullName}");
        }
        
        public static externalDTO.Comment MapFromBLL(internalDTO.Comment comment)
        {
            var res = comment == null ? null : new externalDTO.Comment()
            {
                Id = comment.Id,
                CommentTitle = comment.CommentTitle,
                CommentBody = comment.CommentBody,
                ProductId = comment.ProductId,
                ProductName = comment.ProductName,
                ShopId = comment.ShopId
            };
            return res;
        }
        
        public static externalDTO.Comment MapFromBLL(internalDTO.DomainLikeDTO.Comment comment)
        {
            var res = comment == null ? null : new externalDTO.Comment()
            {
                Id = comment.Id,
                CommentTitle = comment.CommentTitle,
                CommentBody = comment.CommentBody,
                ProductId = comment.ProductId,
                ShopId = comment.ShopId
            };
            return res;
        }
        
        public static internalDTO.DomainLikeDTO.Comment MapFromExternal(externalDTO.Comment comment)
        {
            var res = comment == null ? null : new internalDTO.DomainLikeDTO.Comment()
            {
                Id = comment.Id,
                CommentTitle = comment.CommentTitle,
                CommentBody = comment.CommentBody,
                ProductId = comment.ProductId,
                ShopId = comment.ShopId
            };
            return res;
        }
        
        public static externalDTO.IdAndNameDTO.CommentIdTitleBody MapFromBLL(internalDTO.IdAndNameDTO.CommentIdTitleBody comment)
        {
            var res = comment == null ? null : new externalDTO.IdAndNameDTO.CommentIdTitleBody()
            {
                Id = comment.Id,
                CommentTitle = comment.CommentTitle,
                CommentBody = comment.CommentBody,
            };
            return res;
        }
        
        public static internalDTO.IdAndNameDTO.CommentIdTitleBody MapFromExternal(externalDTO.IdAndNameDTO.CommentIdTitleBody comment)
        {
            var res = comment == null ? null : new internalDTO.IdAndNameDTO.CommentIdTitleBody()
            {
                Id = comment.Id,
                CommentTitle = comment.CommentTitle,
                CommentBody = comment.CommentBody,
            };
            return res;
        }

    }
}