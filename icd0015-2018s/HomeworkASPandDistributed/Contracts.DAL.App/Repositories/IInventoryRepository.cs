using System.Collections.Generic;
using System.Threading.Tasks;
using ee.itcollege.nikita.Contracts.DAL.Base.Repositories;
using DALAppDTO = DAL.App.DTO;


namespace Contracts.DAL.App.Repositories
{
    public interface IInventoryRepository : IInventoryRepository<DALAppDTO.DomainLikeDTO.Inventory>
    {
        Task<List<DALAppDTO.InventoryWithProductCount>> GetByShopAsync(int? shopId, string search, int? pageIndex, int? pageSize);
        Task<DALAppDTO.InventoryWithProductCount> FindByShopAsyncAndIdAsync (int id, int? shopId);

    }
    
    public interface IInventoryRepository<TDALEntity> : IBaseRepository<TDALEntity>
        where TDALEntity: class, new()
    {
        Task<int> CountDataAmount(int? shopId, string search);
        Task<int> CountDataAmount(string search);
        Task<List<TDALEntity>> AllAsyncByShop(int? shopId, string order, string searchFor, int? pageIndex, int? pageSize);
        Task<List<TDALEntity>> AllAsync(string order, string searchFor, int? pageIndex, int? pageSize);
        Task<bool> IsInventoryEmpty(int id);
    }
}