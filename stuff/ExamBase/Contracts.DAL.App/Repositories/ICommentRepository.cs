using System.Collections.Generic;
using System.Threading.Tasks;
using Domain;
using ee.itcollege.nikita.Contracts.DAL.Base.Repositories;
using DALAppDTO = DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface ICommentRepository : IBaseRepository<Comment>
    {
        Task<List<Comment>> AllAsyncWithSearch(int userId, string search);
        Task<Comment> FindAsyncById(int id, int userId);
        Task<List<DALAppDTO.Comment>> AllAsyncWithSearchAPI(int userId, string search);
        Task<DALAppDTO.Comment> FindAsyncByIdAPI(int id, int userId);
    }
}