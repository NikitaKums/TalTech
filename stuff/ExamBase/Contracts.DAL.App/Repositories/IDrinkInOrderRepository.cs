using System.Collections.Generic;
using System.Threading.Tasks;
using Domain;
using ee.itcollege.nikita.Contracts.DAL.Base.Repositories;
using DALAppDTO = DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IDrinkInOrderRepository : IBaseRepository<DrinkInOrder>
    {
        Task<List<DrinkInOrder>> AllAsyncByUserId(int userId);
        Task<DrinkInOrder> FindAsyncById(int? id, int userId);
        Task<List<DALAppDTO.DrinkInOrder>> AllAsyncWithSearchAPI(int userId, string search);
        Task<DALAppDTO.DrinkInOrder> FindAsyncByIdAPI(int id, int userId);
    }
}