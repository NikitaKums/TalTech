using System.Collections.Generic;
using System.Threading.Tasks;
using ee.itcollege.nikita.Contracts.DAL.Base.Repositories;
using DALAppDTO = DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IReturnRepository : IReturnRepository<DALAppDTO.DomainLikeDTO.Return>
    {
        Task<List<DALAppDTO.ReturnWithProductCount>> GetAllWithProductsReturnedByShopAsync(int? shopId, string search, int? pageIndex, int? pageSize);
        Task<DALAppDTO.ReturnWithProductCount> FindWithProductsReturnedByIdAndShopAsync(int id, int? shopId);
        Task<List<DALAppDTO.IdAndNameDTO.ReturnIdName>> GetAllIdAndDescAsyncByShopDTO(int? shopId);
    }
    
    public interface IReturnRepository<TDALEntity> : IBaseRepository<TDALEntity>
        where TDALEntity: class, new()
    {
        Task<int> CountDataAmount(int? shopId, string search);
        Task<int> CountDataAmount(string search);
        Task<List<TDALEntity>> AllAsyncByShop(int? shopId, string order, string searchFor, int? pageIndex, int? pageSize);
        Task<List<TDALEntity>> AllAsync(string order, string searchFor, int? pageIndex, int? pageSize);
        //Task<List<TDALEntity>> GetAllWithProductsReturnedAsync();
    }
}