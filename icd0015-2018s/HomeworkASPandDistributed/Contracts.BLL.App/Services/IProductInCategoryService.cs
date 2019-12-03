using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using ee.itcollege.nikita.Contracts.BLL.Base.Services;
using BLLAppDTO = BLL.App.DTO;

namespace Contracts.BLL.App.Services
{
    public interface IProductInCategoryService : IBaseEntityService<BLLAppDTO.DomainLikeDTO.ProductInCategory>, IProductInCategoryRepository<BLLAppDTO.DomainLikeDTO.ProductInCategory>
    {
        Task<List<BLLAppDTO.ProductInCategory>> AllAsyncByShopDTO(int? shopId);
        Task<BLLAppDTO.ProductInCategory> GetAsyncByShopAndIdDTO(int id, int? shopId);
        Task<List<BLLAppDTO.IdAndNameDTO.ProductIdName>> AllAsyncByCategoryIdAndShopId(int categoryId, int? shopId);
    }
}