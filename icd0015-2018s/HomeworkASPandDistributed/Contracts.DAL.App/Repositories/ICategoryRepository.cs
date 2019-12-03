using System.Collections.Generic;
using System.Threading.Tasks;
using DALAppDTO = DAL.App.DTO;
using ee.itcollege.nikita.Contracts.DAL.Base.Repositories;

namespace Contracts.DAL.App.Repositories
{
    public interface ICategoryRepository : ICategoryRepository<DALAppDTO.DomainLikeDTO.Category>
    {
        Task<List<DALAppDTO.CategoryWithProductCount>> GetAllWithProductCountForShopAsync(int? shopId, string search, int? pageIndex, int? pageSize);
        Task<DALAppDTO.CategoryWithProductCount> GetByIndexAndShop(int categoryId, int? shopId);
    }
    
    public interface ICategoryRepository<TDALEntity> : IBaseRepository<TDALEntity>
        where TDALEntity: class, new()
    {
        Task<int> CountDataAmount(int? shopId, string search);
        Task<int> CountDataAmount(string search);
        Task<List<TDALEntity>> AllAsyncByShop(int? shopId, string order, string searchFor, int? pageIndex, int? pageSize);
        Task<List<TDALEntity>> AllAsync(string order, string searchFor, int? pageIndex, int? pageSize);
    }
}