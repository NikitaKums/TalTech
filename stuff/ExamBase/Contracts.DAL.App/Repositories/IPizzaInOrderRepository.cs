using System.Collections.Generic;
using System.Threading.Tasks;
using Domain;
using ee.itcollege.nikita.Contracts.DAL.Base.Repositories;
using DALAppDTO = DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IPizzaInOrderRepository : IBaseRepository<PizzaInOrder>
    {
        Task<List<PizzaInOrder>> AllAsyncWithSearch(int userId);
        Task<PizzaInOrder> FindAsyncById(int? id, int userId);
        Task<List<DALAppDTO.PizzaInOrder>> AllAsyncWithSearchAPI(int userId, string search);
        Task<DALAppDTO.PizzaInOrder> FindAsyncByIdAPI(int id, int userId);
    }
}