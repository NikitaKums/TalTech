using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using BLL.App.Mappers;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Domain;
using ee.itcollege.nikita.BLL.Base.Services;
using Comment = BLL.App.DTO.DomainLikeDTO.Comment;

namespace BLL.App.Services
{
    public class CommentService : BaseEntityService<BLL.App.DTO.DomainLikeDTO.Comment, DAL.App.DTO.DomainLikeDTO.Comment, IAppUnitOfWork>, ICommentService
    {
        public CommentService(IAppUnitOfWork uow) : base(uow, new CommentMapper())
        {
            ServiceRepository = Uow.Comments;

        }

        /*public override async Task<IEnumerable<Comment>> AllAsync()
        {
            return await UOW.Comments.AllAsync();
        }

        public override async Task<Comment> FindAsync(params object[] id)
        {
            return await UOW.Comments.FindAsync(id);
        }

        public async Task<IEnumerable<Comment>> AllAsyncByShop(int? shopId)
        {
            return await UOW.Comments.AllAsyncByShop(shopId);
        }

        public async Task<IEnumerable<CommentDTO>> GetAllWithProductByShopAsync(int? shopId)
        {
            return await UOW.Comments.GetAllWithProductByShopAsync(shopId);
        }

        public async Task<CommentDTO> GetCommentById(int id)
        {
            return await UOW.Comments.GetCommentById(id);
        }

        public async Task<CommentDTO> GetCommentByIdAndShop(int id, int? shopId)
        {
            return await UOW.Comments.GetCommentByIdAndShop(id, shopId);
        }*/
        
        public async Task<int> CountDataAmount(string search)
        {
            return await Uow.Comments.CountDataAmount(search);
        }
        
        public override async Task<List<Comment>> AllAsync()
        {
            return (await Uow.Comments.AllAsync()).Select(e => CommentMapper.MapFromDAL(e)).ToList();

        }
        
        public async Task<int> CountDataAmount(int? shopId, string search)
        {
            return await Uow.Comments.CountDataAmount(shopId, search);
        }

        public override async Task<Comment> FindAsync(params object[] id)
        {
            return CommentMapper.MapFromDAL( await Uow.Comments.FindAsync(id));
        }

        public async Task<DTO.Comment> GetCommentById(int id)
        {
            return CommentMapper.MapFromDAL(await Uow.Comments.GetCommentById(id));
        }

        public async Task<DTO.Comment> GetCommentByIdAndShop(int id, int? shopId)
        {
            return CommentMapper.MapFromDAL(await Uow.Comments.GetCommentByIdAndShop(id, shopId));
        }

        public async Task<List<DTO.Comment>> GetAllWithProductByShopAsync(int? shopId, string search, int? pageIndex, int? pageSize)
        {
            return (await Uow.Comments.GetAllWithProductByShopAsync(shopId, search, pageIndex, pageSize)).Select(e => CommentMapper.MapFromDAL(e)).ToList();
        }

        public async Task<List<Comment>> AllAsyncByShop(int? shopId, string order, string searchFor, int? pageIndex, int? pageSize)
        {
            return (await Uow.Comments.AllAsyncByShop(shopId, order, searchFor, pageIndex, pageSize)).Select(e => CommentMapper.MapFromDAL(e)).ToList();
        }

        public async Task<List<Comment>> AllAsync(string order, string searchFor, int? pageIndex, int? pageSize)
        {
            return (await Uow.Comments.AllAsync(order, searchFor, pageIndex, pageSize)).Select(e => CommentMapper.MapFromDAL(e)).ToList();
        }
    }
}