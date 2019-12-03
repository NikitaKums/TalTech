using System.Collections.Generic;
using System.Threading.Tasks;
using ee.itcollege.nikita.Contracts.DAL.Base.Repositories;
using DALAppDTO = DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{

    public interface IOrderRepository : IOrderRepository<DALAppDTO.DomainLikeDTO.Order>
    {
        Task<List<DALAppDTO.OrderWithProductCount>> GetAllByShopDTOAsync(int? shopId, string search, int? pageIndex, int? pageSize);
        Task<DALAppDTO.OrderWithProductCount> FindByShopAndIdAsync(int id, int? shopId);
    }
    
    public interface IOrderRepository<TDALEntity> : IBaseRepository<TDALEntity>
        where TDALEntity: class, new()
    {
        Task<int> CountDataAmount(int? shopId, string search);
        Task<int> CountDataAmount(string search);
        Task<List<TDALEntity>> AllAsyncByShop(int? shopId, string order, string searchFor, int? pageIndex, int? pageSize);
        Task<List<TDALEntity>> AllAsync(string order, string searchFor, int? pageIndex, int? pageSize);
        Task<int> CountOrdersInShop(int? shopId);
        Task<TDALEntity> FindAsyncById(int id);
    }
}