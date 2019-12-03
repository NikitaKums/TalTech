using System.Collections.Generic;
using System.Threading.Tasks;
using ee.itcollege.nikita.Contracts.DAL.Base.Repositories;
using DALAppDTO = DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IProductWithDefectRepository : IProductWithDefectRepository<DALAppDTO.DomainLikeDTO.ProductWithDefect>
    {
        Task<List<DALAppDTO.ProductWithDefect>> AllAsyncByShopDTO(int? shopId);
        Task<DALAppDTO.ProductWithDefect> GetAsyncByShopAndIdDTO(int id, int? shopId);
        Task<List<DALAppDTO.DomainLikeDTO.ProductWithDefect>> AllAsyncByDefectId(int defectId);
        Task<List<DALAppDTO.IdAndNameDTO.ProductIdName>> AllAsyncByDefectIdAndShopId(int defectId, int? shopId);
        Task<List<DALAppDTO.ProductWithDefect>> AllAsyncByShopAndDefectIdDTO(int defectId, int? shopId);
    }
    
    public interface IProductWithDefectRepository<TDALEntity> : IBaseRepository<TDALEntity>
        where TDALEntity : class, new()
    {
        Task<List<TDALEntity>> AllAsyncByShop(int? shopId);
        Task<int> CountDefectItems(int orderId);
    }
}