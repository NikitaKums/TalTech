using System.Collections.Generic;
using System.Threading.Tasks;
using ee.itcollege.nikita.Contracts.DAL.Base.Repositories;
using DALAppDTO = DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IAppUserRepository : IAppUserRepository<DALAppDTO.DomainLikeDTO.Identity.AppUser>
    {
        Task<DALAppDTO.Identity.AppUser> GetUserInfo(int userId);
    }

    public interface IAppUserRepository<TDALEntity> : IBaseRepository<TDALEntity> 
        where TDALEntity : class, new()
    {
        Task<List<TDALEntity>> AllAsync(string order, string searchFor);
        Task<List<TDALEntity>> GetUserById(int userId);
        Task<List<TDALEntity>> AllAsyncByShop(int? shopId, string order, string searchFor);
        Task<int> CountUsersInShop(int? shopId);
        Task<int> CountAllUsers();
    }
}