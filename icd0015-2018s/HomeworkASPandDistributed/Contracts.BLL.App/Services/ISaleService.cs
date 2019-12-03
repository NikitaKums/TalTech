using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using ee.itcollege.nikita.Contracts.BLL.Base.Services;
using BLLAppDTO = BLL.App.DTO;

namespace Contracts.BLL.App.Services
{
    public interface ISaleService : IBaseEntityService<BLLAppDTO.DomainLikeDTO.Sale>, ISaleRepository<BLLAppDTO.DomainLikeDTO.Sale>
    {
        Task<List<BLLAppDTO.SaleWithProductCount>> GetAsyncByShopAndUserIdDTO(int userId, int? shopId);
        Task<List<BLLAppDTO.SaleWithProductCount>> AllAsyncByShopDTO(int? shopId, string search, int? pageIndex, int? pageSize);
        Task<BLLAppDTO.SaleWithProductCount> GetAsyncByShopAndIdDTO(int id, int? shopId);
        Task DeleteWithoutRestoration(int saleId);
        Task DeleteWithRestoration(int saleId);
    }
}