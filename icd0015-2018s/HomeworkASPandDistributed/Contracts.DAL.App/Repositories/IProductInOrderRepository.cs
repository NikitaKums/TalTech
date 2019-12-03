using System.Collections.Generic;
using System.Threading.Tasks;
using ee.itcollege.nikita.Contracts.DAL.Base.Repositories;
using DALAppDTO = DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IProductInOrderRepository : IProductInOrderRepository<DALAppDTO.DomainLikeDTO.ProductInOrder>
    {
        Task<List<DALAppDTO.ProductInOrder>> AllAsyncByShopDTO(int? shopId);
        Task<DALAppDTO.ProductInOrder> GetAsyncByShopAndIdDTO(int id, int? shopId);
        Task<List<DALAppDTO.DomainLikeDTO.ProductInOrder>> AllAsyncByOrderId(int orderId);
        Task<List<DALAppDTO.OrderReceived>> FindOrdersReceivedByOrderId(int orderId, int? shopId);
        Task<List<DALAppDTO.ProductInOrder>> AllAsyncByShopAndOrderId(int id, int? shopId);
    }
    
    public interface IProductInOrderRepository<TDALEntity> : IBaseRepository<TDALEntity>
        where TDALEntity: class, new()
    {
        Task<List<TDALEntity>> AllAsyncByShop(int? shopId);

    }
}