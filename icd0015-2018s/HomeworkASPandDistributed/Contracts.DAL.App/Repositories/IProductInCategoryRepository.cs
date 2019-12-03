using System.Collections.Generic;
using System.Threading.Tasks;
using ee.itcollege.nikita.Contracts.DAL.Base.Repositories;
using DALAppDTO = DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IProductInCategoryRepository : IProductInCategoryRepository<DALAppDTO.DomainLikeDTO.ProductInCategory>
    {
        Task<List<DALAppDTO.ProductInCategory>> AllAsyncByShopDTO(int? shopId);
        Task<DALAppDTO.ProductInCategory> GetAsyncByShopAndIdDTO(int id, int? shopId);
        Task<List<DALAppDTO.IdAndNameDTO.ProductIdName>> AllAsyncByCategoryIdAndShopId(int categoryId, int? shopId);
    }

    public interface IProductInCategoryRepository<TDALEntity> : IBaseRepository<TDALEntity>
        where TDALEntity: class, new()
    {
        Task<List<TDALEntity>> AllAsyncByCategoryId(int categoryId);
        Task<List<TDALEntity>> AllAsyncByShop(int? shopId);
    }
}