using System.Collections.Generic;
using System.Threading.Tasks;
using Domain;
using ee.itcollege.nikita.Contracts.DAL.Base.Repositories;
using DALAppDTO = DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IToppingOnPizzaRepository : IBaseRepository<ToppingOnPizza>
    {
        Task<List<ToppingOnPizza>> AllAsyncWithSearch(int userId, string search);
        Task<ToppingOnPizza> FindAsyncById(int? id, int userId);
        Task<List<DALAppDTO.ToppingOnPizza>> AllAsyncWithSearchAPI(int userId, string search);
        Task<DALAppDTO.ToppingOnPizza> FindAsyncByIdAPI(int id, int userId);
    }
}