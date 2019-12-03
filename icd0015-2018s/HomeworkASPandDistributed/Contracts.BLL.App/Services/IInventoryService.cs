using System.Collections.Generic;
using System.Threading.Tasks;
using BLL.App.DTO.DomainLikeDTO;
using Contracts.DAL.App.Repositories;
using ee.itcollege.nikita.Contracts.BLL.Base.Services;
using BLLAppDTO = BLL.App.DTO;

namespace Contracts.BLL.App.Services
{
    public interface IInventoryService : IBaseEntityService<BLLAppDTO.DomainLikeDTO.Inventory>, IInventoryRepository<BLLAppDTO.DomainLikeDTO.Inventory>
    {
        Task<List<BLLAppDTO.InventoryWithProductCount>> GetByShopAsync(int? shopId, string search, int? pageIndex, int? pageSize);
        Task<BLLAppDTO.InventoryWithProductCount> FindByShopAsyncAndIdAsync (int id, int? shopId);
    }
}