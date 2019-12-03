using System.Collections.Generic;
using System.Threading.Tasks;
using DALAppDTO = DAL.App.DTO;
using ee.itcollege.nikita.Contracts.DAL.Base.Repositories;

namespace Contracts.DAL.App.Repositories
{

    public interface ICommentRepository : ICommentRepository<DALAppDTO.DomainLikeDTO.Comment>
    {
        Task<DALAppDTO.Comment> GetCommentById(int id);
        Task<DALAppDTO.Comment> GetCommentByIdAndShop(int id, int? shopId);
        Task<List<DALAppDTO.Comment>> GetAllWithProductByShopAsync(int? shopId, string search, int? pageIndex, int? pageSize);
    }
    
    public interface ICommentRepository<TDALEntity> : IBaseRepository<TDALEntity>
        where TDALEntity: class, new()
    {
        Task<int> CountDataAmount(int? shopId, string search);
        Task<int> CountDataAmount(string search);
        Task<List<TDALEntity>> AllAsyncByShop(int? shopId, string order, string searchFor, int? pageIndex, int? pageSize);
        Task<List<TDALEntity>> AllAsync(string order, string searchFor, int? pageIndex, int? pageSize);
    }
}