using System.Collections.Generic;
using System.Threading.Tasks;
using ee.itcollege.nikita.Contracts.DAL.Base.Repositories;
using DALAppDTO = DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{

    public interface IProductRepository : IProductRepository<DALAppDTO.DomainLikeDTO.Product>
    {
        Task<DALAppDTO.ProductWithCounts> FindByShopAndId(int id, int? shopId);
        Task<List<DALAppDTO.ProductWithCounts>> GetProductIdNameByShopInInventoryDTO(int? shopId);
        Task<List<DALAppDTO.ProductWithCounts>> AllAsyncByShopDTO(int? shopId, string search, int? pageIndex, int? pageSize);
        Task<List<DALAppDTO.ProductWithCounts>> GetProductIdNameByShopDTO(int? shopId);
    }
    
    public interface IProductRepository<TDALEntity> : IBaseRepository<TDALEntity>
        where TDALEntity : class, new()
    {
        Task<int> CountDataAmount(int? shopId, string search);
        Task<int> CountDataAmount(string search);
        Task<TDALEntity> FindProductInfoAsync(int id);
        Task<List<TDALEntity>> AllAsyncByShopForDropDown(int? shopId);
        Task<List<TDALEntity>> AllAsyncByShop(int? shopId, string order, string searchFor, int? pageIndex, int? pageSize);
        Task<List<TDALEntity>> AllAsync(string order, string searchFor, int? pageIndex, int? pageSize);
        Task<int> CountProductsInShop(int? shopId);
        Task<List<TDALEntity>> AllAsyncByShopAndInInventory(int? shopId);

    }
}