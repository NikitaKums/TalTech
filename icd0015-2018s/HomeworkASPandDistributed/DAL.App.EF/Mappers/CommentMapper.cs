using System;
using Domain;
using ee.itcollege.nikita.Contracts.DAL.Base.Mappers;

namespace DAL.App.EF.Mappers
{
    public class CommentMapper : IBaseDALMapper
    {
        public TOutObject Map<TOutObject>(object inObject) where TOutObject : class
        {
            if (typeof(TOutObject) == typeof(DAL.App.DTO.DomainLikeDTO.Comment))
            {
                return MapFromDomain((Domain.Comment) inObject) as TOutObject;
            }

            if (typeof(TOutObject) == typeof(Domain.Comment))
            {
                return MapFromDAL((DAL.App.DTO.DomainLikeDTO.Comment) inObject) as TOutObject;
            }

            throw new InvalidCastException($"No conversion from {inObject.GetType().FullName} to {typeof(TOutObject).FullName}");
        }
        
        public static DAL.App.DTO.DomainLikeDTO.Comment MapFromDomain(Domain.Comment comment)
        {
            var res = comment == null ? null : new DAL.App.DTO.DomainLikeDTO.Comment
            {
                Id = comment.Id,
                CommentTitle = comment.CommentTitle.Translate(),
                CommentBody = comment.CommentBody.Translate(),
                ProductId = comment.ProductId,
                Product = ProductMapper.MapFromDomain(comment.Product),
                ShopId = comment.ShopId,
                Shop = ShopMapper.MapFromDomain(comment.Shop)
            };
            
            return res;
        }

        public static Domain.Comment MapFromDAL(DAL.App.DTO.DomainLikeDTO.Comment comment)
        {
            var res = comment == null ? null : new Domain.Comment
            {
                Id = comment.Id,
                CommentTitle = new MultiLangString(comment.CommentTitle),
                CommentBody = new MultiLangString(comment.CommentBody),
                ProductId = comment.ProductId,
                Product = ProductMapper.MapFromDAL(comment.Product),
                ShopId = comment.ShopId,
                Shop = ShopMapper.MapFromDAL(comment.Shop)
            };
            
            return res;
        }
    }
}