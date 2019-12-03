using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using ee.itcollege.nikita.Contracts.BLL.Base.Services;
using BLLAppDTO = BLL.App.DTO;

namespace Contracts.BLL.App.Services
{
    public interface ICategoryService : IBaseEntityService<BLLAppDTO.DomainLikeDTO.Category>, ICategoryRepository<BLLAppDTO.DomainLikeDTO.Category>
    {
        Task<List<BLLAppDTO.CategoryWithProductCount>> GetAllWithProductCountForShopAsync(int? shopId, string search, int? pageIndex, int? pageSize);
        Task<BLLAppDTO.CategoryWithProductCount> GetByIndexAndShop(int categoryId, int? shopId);
        Task DeleteCategory(int categoryId);
    }
}