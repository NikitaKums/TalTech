using System.Collections.Generic;
using System.Threading.Tasks;
using Domain;
using ee.itcollege.nikita.Contracts.DAL.Base.Repositories;
using DALAppDTO = DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IToppingRepository : IBaseRepository<Topping>
    {
        Task<List<Topping>> AllAsyncWithSearch(string search);
        Task<Topping> FindAsyncById(int? id);
        Task<List<DALAppDTO.Topping>> AllAsyncWithSearchAPI(string search);
        Task<DALAppDTO.Topping> FindAsyncByIdAPI(int id);
    }
}