using System.Collections.Generic;
using System.Threading.Tasks;
using Domain;
using ee.itcollege.nikita.Contracts.DAL.Base.Repositories;
using DALAppDTO = DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IPizzaRepository : IBaseRepository<Pizza>
    {
        Task<List<Pizza>> AllAsyncWithSearch(string search);
        Task<Pizza> FindAsyncById(int? id);
        Task<List<DALAppDTO.Pizza>> AllAsyncWithSearchAPI(string search);
        Task<DALAppDTO.Pizza> FindAsyncByIdAPI(int id);
    }
}