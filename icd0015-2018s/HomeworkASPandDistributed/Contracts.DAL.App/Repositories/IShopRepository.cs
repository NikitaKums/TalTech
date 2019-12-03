using System.Collections.Generic;
using System.Threading.Tasks;
using ee.itcollege.nikita.Contracts.DAL.Base.Repositories;
using DALAppDTO = DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IShopRepository : IShopRepository<DALAppDTO.DomainLikeDTO.Shop>
    {
        Task<DALAppDTO.ShopWithCounts> GetShopByShopId(int? shopId);
        Task<List<DALAppDTO.ShopWithCounts>> GetAllWithCountsAsync();
    }
    
    public interface IShopRepository<TDALEntity> : IBaseRepository<TDALEntity>
        where TDALEntity : class, new()
    {
        Task<int> CountDataAmount(string search);
        Task<List<TDALEntity>> AllAsync(string order, string searchFor, int? pageIndex, int? pageSize);      
        Task<List<TDALEntity>> GetShopByUserShopIdForDropDown(int? shopId);
        Task<List<TDALEntity>> GetShopByUserShopId(int? shopId);
    }
}