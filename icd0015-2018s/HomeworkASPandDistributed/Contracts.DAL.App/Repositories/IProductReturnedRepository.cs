using System.Collections.Generic;
using System.Threading.Tasks;
using ee.itcollege.nikita.Contracts.DAL.Base.Repositories;
using DALAppDTO = DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IProductReturnedRepository : IProductReturnedRepository<DALAppDTO.DomainLikeDTO.ProductReturned>
    {
        Task<List<DALAppDTO.ProductReturned>> AllAsyncByShopDTO(int? shopId);
        Task<DALAppDTO.ProductReturned> GetAsyncByShopAndIdDTO(int id, int? shopId);
        Task<List<DALAppDTO.ProductReturned>> AllAsyncByShopAndIdDTO(int id, int? shopId);
    }
    
    public interface IProductReturnedRepository<TDALEntity> : IBaseRepository<TDALEntity>
        where TDALEntity : class, new()
    {
        Task<List<TDALEntity>> AllAsyncByReturnId(int returnId);
        Task<List<TDALEntity>> AllAsyncByShop(int? shopId);
        Task<int> CountProductsInReturn(int returnId);
    }
}