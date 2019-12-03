using System.Collections.Generic;
using System.Threading.Tasks;
using Domain;
using ee.itcollege.nikita.Contracts.DAL.Base.Repositories;
using DALAppDTO = DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IOrderRepository : IBaseRepository<Order>
    {
        Task<List<Order>> AllAsyncWithSearch(int userId, string search);
        Task<List<Order>> AllAsyncWithSearch(string search);
        Task<Order> FindAsyncById(int? id, int userId);
        Task<Order> FindAsyncById(int? id);
        Task<int> FindOrderPriceAsyncById(int? id, int userId);
        Task<List<DALAppDTO.Order>> AllAsyncWithSearchAPI(int userId, string search);
        Task<DALAppDTO.Order> FindAsyncByIdAPI(int id, int userId);
    }
}