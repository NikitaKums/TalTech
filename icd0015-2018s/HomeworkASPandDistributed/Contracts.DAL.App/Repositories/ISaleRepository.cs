using System.Collections.Generic;
using System.Threading.Tasks;
using ee.itcollege.nikita.Contracts.DAL.Base.Repositories;
using DALAppDTO = DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface ISaleRepository : ISaleRepository<DALAppDTO.DomainLikeDTO.Sale>
    {
        Task<List<DALAppDTO.SaleWithProductCount>> GetAsyncByShopAndUserIdDTO(int userId, int? shopId);
        Task<List<DALAppDTO.SaleWithProductCount>> AllAsyncByShopDTO(int? shopId, string search, int? pageIndex, int? pageSize);
        Task<DALAppDTO.SaleWithProductCount> GetAsyncByShopAndIdDTO(int id, int? shopId);
    }
    
    public interface ISaleRepository<TDALEntity> : IBaseRepository<TDALEntity>
        where TDALEntity : class, new()
    {
        Task<int> CountDataAmount(int? shopId, string search);
        Task<int> CountDataAmount(string search);
        Task<List<TDALEntity>> AllAsyncByShop(int? shopId, string order, string searchFor, int? pageIndex, int? pageSize);
        Task<List<TDALEntity>> AllAsync(string order, string searchFor, int? pageIndex, int? pageSize);
        Task<List<TDALEntity>> AllAsyncByShopAndUserId(int? shopId, int userId);
        Task<Dictionary<string, decimal?>> GetSaleAmounts(int? shopId);
    }
}