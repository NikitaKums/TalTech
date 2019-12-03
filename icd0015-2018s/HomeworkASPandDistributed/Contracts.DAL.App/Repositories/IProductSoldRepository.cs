using System.Collections.Generic;
using System.Threading.Tasks;
using ee.itcollege.nikita.Contracts.DAL.Base.Repositories;
using DALAppDTO = DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IProductSoldRepository : IProductSoldRepository<DALAppDTO.DomainLikeDTO.ProductSold>
    {
        Task<List<DALAppDTO.ProductSold>> AllAsyncByShopDTO(int? shopId);
        Task<DALAppDTO.ProductSold> GetAsyncByShopAndIdDTO(int id, int? shopId);
        Task<List<DALAppDTO.ProductSold>> AllAsyncByShopAndSaleId(int id, int? shopId);
    }
    
    public interface IProductSoldRepository<TDALEntity> : IBaseRepository<TDALEntity>
        where TDALEntity : class, new()
    {
        Task<List<TDALEntity>> FindBySaleId(int saleId);
        Task<List<TDALEntity>> AllAsyncByShop(int? shopId);
        Task<int> GetQuantity(int id);
        Task<int> CountProductsInSale(int id);
    }
}