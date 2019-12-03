using System.Collections.Generic;
using System.Threading.Tasks;
using BLL.App.DTO.DomainLikeDTO;
using Contracts.DAL.App.Repositories;
using ee.itcollege.nikita.Contracts.BLL.Base.Services;
using BLLAppDTO = BLL.App.DTO;

namespace Contracts.BLL.App.Services
{
    public interface IOrderService : IBaseEntityService<BLLAppDTO.DomainLikeDTO.Order>, IOrderRepository<BLLAppDTO.DomainLikeDTO.Order>
    {
        Task<List<BLLAppDTO.OrderWithProductCount>> GetAllByShopDTOAsync(int? shopId, string search, int? pageIndex, int? pageSize);
        Task<BLLAppDTO.OrderWithProductCount> FindByShopAndIdAsync(int id, int? shopId);
        Task<bool> ProcessReceivedOrder(int id);
        Task DeleteOrderWithProducts(int id);
    }
}