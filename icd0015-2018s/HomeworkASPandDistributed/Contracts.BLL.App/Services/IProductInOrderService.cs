using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using ee.itcollege.nikita.Contracts.BLL.Base.Services;
using BLLAppDTO = BLL.App.DTO;

namespace Contracts.BLL.App.Services
{
    public interface IProductInOrderService : IBaseEntityService<BLLAppDTO.DomainLikeDTO.ProductInOrder>, IProductInOrderRepository<BLLAppDTO.DomainLikeDTO.ProductInOrder>
    {
        Task<List<BLLAppDTO.ProductInOrder>> AllAsyncByShopDTO(int? shopId);
        Task<BLLAppDTO.ProductInOrder> GetAsyncByShopAndIdDTO(int id, int? shopId);
        Task<List<BLLAppDTO.DomainLikeDTO.ProductInOrder>> AllAsyncByOrderId(int orderId);
        Task<List<BLLAppDTO.OrderReceived>> FindOrdersReceivedByOrderId(int orderId, int? shopId);
        Task ProductInOrderReceived(int id);
        Task<List<BLLAppDTO.ProductInOrder>> AllAsyncByShopAndOrderId(int id, int? shopId);
    }
}