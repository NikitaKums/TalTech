using System.Collections.Generic;
using System.Threading.Tasks;
using Domain;
using ee.itcollege.nikita.Contracts.DAL.Base.Repositories;
using DALAppDTO = DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IDeliveryRepository : IBaseRepository<Delivery>
    {
        Task<List<Delivery>> AllAsyncWithSearch(string search);
        Task<Delivery> FindAsyncById(int? id);
        Task<List<DALAppDTO.Delivery>> AllAsyncWithSearchAPI(string search);
        Task<DALAppDTO.Delivery> FindAsyncByIdAPI(int id);
    }
}