using System.Collections.Generic;
using System.Threading.Tasks;
using Domain;
using ee.itcollege.nikita.Contracts.DAL.Base.Repositories;
using DALAppDTO = DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IDrinkRepository : IBaseRepository<Drink>
    {
        Task<List<Drink>> AllAsyncWithSearch( string search);
        Task<Drink> FindAsyncById(int? id);
        Task<List<DALAppDTO.Drink>> AllAsyncWithSearchAPI(string search);
        Task<DALAppDTO.Drink> FindAsyncByIdAPI(int id);
    }
}